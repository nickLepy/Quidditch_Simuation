using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;


public class Cognard : MonoBehaviour
{

    private bool _estFrappe;
    public bool EstFrappe { get { return _estFrappe; } }

    private float _vitesse;
    private Player _cible;
    private Vector3 _velocityBatteur;

    private static GameObject _objet = null;
    public static GameObject Objet
    {
        get
        {
            if (_objet == null) _objet = GameObject.Find("Cognard");
            return _objet;
        }
    }

    private void Start()
    {
        _vitesse = 40;
        _cible = null;
        _velocityBatteur = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        
        

        Deplacement.CheckBordures(transform);

        //Fonction principale du cognard : suive et frapper les joueurs
        if(_cible == null)
        {
            TrouverCible();
        }
        else
        {
            if(Math.Abs(Vector3.Distance(_cible.transform.position,transform.position)) > 10)
            {
                TrouverCible();
            }
            _vitesse = (float)(40 * ((Math.Cos(Time.time) + 1) / 4) + 1);
            if (!_estFrappe)
                GetComponent<Rigidbody>().velocity = Deplacement.Direction(transform.position, _cible.transform.position, (int)_vitesse);
            else
                GetComponent<Rigidbody>().velocity = _velocityBatteur;
        }

        if (Deplacement.CollisionBordure(transform))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    private void TrouverCible()
    {
        float minDist = -1;
        foreach(Player p in GameObject.FindObjectsOfType<Player>())
        {
            if(p.Joueur.Poste != Poste.GARDIEN)
            {
                float distance = Math.Abs(Vector3.Distance(p.transform.position, transform.position));
                if (minDist == -1 || minDist > distance)
                {
                    minDist = distance;
                    _cible = p;
                }
            }
            
        }
    }

    public void Frapper(Player p, Player cible)
    {
        Debug.Log("Cognard frappé par " + p.Joueur.Nom);

        //p.ConvoiteCognard = false;
        _velocityBatteur = Deplacement.Direction(transform.position, cible.transform.position, p.Joueur.Precision) ;
        StartCoroutine(Battu());
        
    }


    private void OnTriggerEnter(Collider other)
    {
        
        Player p = null;
        try
        {
            p = other.transform.parent.GetComponent<Player>();
        }
        catch
        {
            Debug.Log("Le cognard est en collision avec quelque chose qui n'est pas un joueur.");
        }

        if (p != null && p.ConvoiteCognard && transform.parent == null)
        {
            Frapper(p,(p.IA as Automate_Batteur).Cible);
        }
        else if(p!=null) //Il est victime du cognard
        {
            Camera.main.gameObject.GetComponent<Main>().Commentaire = "Oh ! " + p.Joueur.Nom + " est frappe par le cognard !";
            Debug.Log("Victime du cognard");
            StartCoroutine(p.EstFrappe());
            other.transform.parent.GetComponent<Rigidbody>().velocity = -GetComponent<Rigidbody>().velocity*2;
        }

    }


    public IEnumerator Battu()
    {
        _estFrappe = true;

        yield return new WaitForSeconds(4);

        _estFrappe = false;
    }

}