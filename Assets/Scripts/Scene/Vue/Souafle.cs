using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Souafle : MonoBehaviour
{
    private static GameObject _objet = null;
    public static GameObject Objet
    {
        get
        {
            if (_objet == null) _objet = GameObject.Find("Souafle");
            return _objet;
        }
    }

    public static GameObject Possesseur
    {
        get
        {
            GameObject res = null;
            if (Objet.transform.parent != null)
                res = Objet.transform.parent.gameObject;
            return res;
        }
    }

    private void FixedUpdate()
    {
        
        if(Possesseur != null)
        {
            transform.localPosition = new Vector3(0, 0, 0.5f);
        }
        else
        {
            if (Deplacement.CollisionBordure(transform))
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        VerifierLimiteTerrain();

        if (transform.position.y < Constantes3D.BORDURE_BASSE)
            transform.position = new Vector3(transform.position.x, Constantes3D.BORDURE_BASSE, transform.position.z) ;
    }

    /// <summary>
    /// Si le souafle dépasse les cages, il est remis au gardien
    /// </summary>
    public void VerifierLimiteTerrain()
    {
        if (transform.position.z < Constantes3D.BORDURE_DERRIERE + 2)
        {
            Player gardienEquipe1 = null;
            foreach(Player p in FindObjectsOfType<Player>())
            {
                if (p.Joueur.Club == Match.LeMatch.Equipe1 && p.Joueur.Poste == Poste.GARDIEN)
                {
                    gardienEquipe1 = p;
                }
            }
            Prendre(gardienEquipe1);
            //transform.parent = gardienEquipe1.transform;
        }

        if (transform.position.z > Constantes3D.BORDURE_DEVANT - 2)
        {
            Player gardienEquipe2 = null;
            foreach (Player p in FindObjectsOfType<Player>())
            {
                if (p.Joueur.Club == Match.LeMatch.Equipe2 && p.Joueur.Poste == Poste.GARDIEN)
                {
                    gardienEquipe2 = p;
                }
            }
            //transform.parent = gardienEquipe2.transform;
            Prendre(gardienEquipe2);
        }
    }

    private DateTime _debut;

    public void Prendre(Player p)
    {
        if(p.Joueur.Poste != Poste.GARDIEN)
            Camera.main.gameObject.GetComponent<Main>().Commentaire = p.Joueur.Nom + " recupere le souafle.";
        //Debug.Log("Changement de main " + p.Joueur.Nom);
        _debut = Match.LeMatch.Temps;

        transform.parent = p.transform;
        transform.localPosition = new Vector3(0, 0, 0.5f);
        transform.GetComponent<Rigidbody>().isKinematic = true;

        p.ConvoiteSouafle = false;
        
        //Ajoute un texte indicateur
        GameObject texte = Resources.Load("Prefabs/TexteNom", typeof(GameObject)) as GameObject;
        texte = Instantiate(texte);
        texte.gameObject.name = "TexteNom";
        texte.transform.parent = p.transform;
        texte.transform.localPosition = new Vector3(0, 4, 0);
        texte.GetComponent<TextMesh>().text = p.Joueur.Prenom + " " + p.Joueur.Nom;        
    }

    public void Lacher()
    {
        DateTime fin = Match.LeMatch.Temps;
        _dernierPossesseur = transform.parent.GetComponent<Player>();
        if (transform.parent.GetComponent<Player>().Joueur.Club == Match.LeMatch.Equipe1)
            Match.LeMatch.AjouterPossession1((int)(fin - _debut).TotalSeconds);

        else
            Match.LeMatch.AjouterPossession2((int)(fin - _debut).TotalSeconds);

        GetComponent<Rigidbody>().isKinematic = false;
        try
        {
            Destroy(transform.parent.Find("TexteNom").gameObject);
        }
        catch
        {
            Debug.Log("Le component TexteNom n'existait pas ...");
        }
        
        transform.parent = null;
    }

    private Player _dernierPossesseur;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Anneau")
        {
            if (other.transform.parent.parent.name == "CageDevant")
            {
                Match.LeMatch.Score2 += 10;
                Match.LeMatch.Evenements.Add(new EvenementMatch(Match.LeMatch.Equipe1, _dernierPossesseur.Joueur, Match.LeMatch.Temps.Minute + 1, TypeEvenement.BUT));
                Camera.main.gameObject.GetComponent<Main>().Commentaire = "But pour " + _dernierPossesseur.Joueur.Club.Nom + "!";

            }

            else if (other.transform.parent.parent.name == "CageDerriere")
            {
                Match.LeMatch.Score1 += 10;
                Match.LeMatch.Evenements.Add(new EvenementMatch(Match.LeMatch.Equipe2, _dernierPossesseur.Joueur, Match.LeMatch.Temps.Minute + 1, TypeEvenement.BUT));
                Camera.main.gameObject.GetComponent<Main>().Commentaire = "But pour " + _dernierPossesseur.Joueur.Club.Nom + "!";
            }
            else
                Debug.Log("Cages inconnues");
            Camera.main.gameObject.GetComponent<Main>().Audio.AjouterSon(Son.BUT, 0.3f, 0.7f, false, UnityEngine.Random.Range(10,20), true, 0);
            Camera.main.gameObject.GetComponent<Main>().Audio.AjouterSon(Son.LRG_STAD_GOAL, 0.6f, 0.9f, false, 0, true, 0);
            if(UnityEngine.Random.Range(1,3)==1)
                Camera.main.gameObject.GetComponent<Main>().Audio.AjouterSon(Son.CHANT, 0.6f, 0.9f, false, 0, true, 3);
            
        }
        else
        {
            Player p = null;
            try
            {
                p = other.transform.parent.GetComponent<Player>();
            }
            catch
            {
                Debug.Log("Le souafle est en collision avec quelque chose qui n'est pas un joueur.");
            }
            
            if (p != null && p.ConvoiteSouafle && transform.parent == null)
            {
                Prendre(p);
                /*if (p.Joueur.Poste == Poste.GARDIEN)
                {
                    Prendre(p);
                }*/
            }
        }
    }
}
