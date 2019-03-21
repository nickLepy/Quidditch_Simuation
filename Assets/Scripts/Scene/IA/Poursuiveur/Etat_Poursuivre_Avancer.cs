using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Etat_Poursuivre_Avancer : Etat
{

    private GameObject _cageCible;

    public Etat_Poursuivre_Avancer(IAutomate automate) : base(automate)
    {
        Automate_Poursuiveur auto = base._automate as Automate_Poursuiveur;
        _cageCible = Deplacement.Cages(Match.LeMatch, auto.Joueur.Joueur.Club);
    }

    private void JouerSon()
    {
        int random = UnityEngine.Random.Range(1, 3);
        switch(random)
        {
            case 1:
                Camera.main.gameObject.GetComponent<Main>().Audio.AjouterSon(Son.LRG_STAD_HAPPY, 0.6f, 0.9f, false, 0, true, 0);

                break;
            case 2:
                Camera.main.gameObject.GetComponent<Main>().Audio.AjouterSon(Son.SML_STAD_HAPPY, 0.6f, 0.9f, false, 0, true, 0);

                break;
            case 3:
                Camera.main.gameObject.GetComponent<Main>().Audio.AjouterSon(Son.SML_STAD_HAPPY2, 0.6f, 0.9f, false, 0, true, 0);

                break;
        }

    }

    public override void Action()
    {
        Automate_Poursuiveur automate = base._automate as Automate_Poursuiveur;
        Vector3 direction = Deplacement.Direction(automate.Joueur.transform.position, _cageCible.transform.position + new Vector3(0,UnityEngine.Random.Range(12,16),0), (int)(automate.Joueur.Joueur.Vitesse*0.9f));
        if (!automate.Joueur.Frappe)
            automate.Joueur.Rigidbody.velocity = direction;
        if (Math.Abs(Vector3.Distance(_cageCible.transform.position, automate.Joueur.transform.position)) < 35 && Souafle.Possesseur == automate.Joueur.gameObject)
        {
            JouerSon();
            Debug.Log("Tir de " + automate.Joueur.Joueur.Nom);
            Match.LeMatch.Tir(automate.Joueur.Joueur);
            Souafle.Objet.GetComponent<Souafle>().Lacher();
            Souafle.Objet.transform.GetComponent<Rigidbody>().velocity = Deplacement.Direction(automate.Joueur.transform.position, Deplacement.Cage(Match.LeMatch, automate.Joueur.Joueur.Club).transform.position, 50);

        }

        //Regarde s'il n'est pas trop proche des joueurs
        float minDist = -1;
        foreach(Player p in GameObject.FindObjectsOfType<Player>())
        {
            if(p.Joueur.Poste == Poste.POURSUIVEUR && p.Joueur.Club != automate.Joueur.Joueur.Club)
            {
                float dist = Math.Abs(Vector3.Distance(p.transform.position, automate.Joueur.transform.position));
                if (minDist == -1 || dist < minDist) minDist = dist;
            }
        }

        //Si un joueur adverse est trop proche, alors il décide de faire une passe au joueur le plus proche
        if(minDist < 3 && minDist > 1)
        {
            float minDistPasse = -1;
            Player cible = null;
            foreach(Player p in GameObject.FindObjectsOfType<Player>())
            {
                if(p.Joueur.Poste == Poste.POURSUIVEUR && p != automate.Joueur && p.Joueur.Club == automate.Joueur.Joueur.Club)
                {
                    float dist = Math.Abs(Vector3.Distance(p.transform.position, automate.Joueur.transform.position));
                    if (minDistPasse == -1 || dist < minDistPasse)
                    {
                        minDistPasse = dist;
                        cible = p;
                    }
                }
            }

            Souafle.Objet.GetComponent<Souafle>().Lacher();
            Souafle.Objet.transform.GetComponent<Rigidbody>().velocity = Deplacement.Direction(automate.Joueur.transform.position, cible.transform.position, 50);
        }
    }

    public override Etat Suivant()
    {
        Automate_Poursuiveur automate = base._automate as Automate_Poursuiveur;
        Etat res = this;
        


        //Si le gardien à le souafle
        if(Souafle.Possesseur != null)
        {
            Player possesseur = Souafle.Possesseur.GetComponent<Player>();
            if (possesseur.Joueur.Poste == Poste.GARDIEN)
            {
                //Son gardien a le souafle
                if (possesseur.Joueur.Club == automate.Joueur.Joueur.Club)
                {
                    res = new Etat_Poursuiveur_SeDemarquerGardien(automate);
                }
                //Le gardien adverse a le souafle
                else
                {
                    res = new Etat_Poursuiveur_Marquage(automate);
                }
            }
        }

        //S'il n'a plus le ballon
        if (Souafle.Objet.transform.parent != automate.Joueur.transform)
            res = new Etat_Poursuivreur_AllerVersSouafle(automate);


        return res;
        
    }
}