using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Un poursuiveur marque un poursuiveur de l'équipe qui a le souafle
/// </summary>
public class Etat_Poursuiveur_Marquage : Etat
{

    private Player _joueurAMarquer;

    public Etat_Poursuiveur_Marquage(IAutomate automate) : base(automate)
    {
        Automate_Poursuiveur a = automate as Automate_Poursuiveur;
        a.Joueur.ConvoiteSouafle = true;
        //Liste les poursuiveurs adverse et en tire un au hasard
        List<Player> aMarquer = new List<Player>();
        foreach(Player p in GameObject.FindObjectsOfType<Player>())
        {
            if (p.Joueur.Poste == Poste.POURSUIVEUR && p.Joueur.Club != a.Joueur.Joueur.Club)
                aMarquer.Add(p);
        }
        _joueurAMarquer = aMarquer[UnityEngine.Random.Range(0, aMarquer.Count - 1)];
    }

    public override void Action()
    {
        Automate_Poursuiveur automate = _automate as Automate_Poursuiveur;
        Vector3 destination = new Vector3(automate.Joueur.transform.position.x + _joueurAMarquer.transform.position.x, automate.Joueur.transform.position.y + _joueurAMarquer.transform.position.y, automate.Joueur.transform.position.z + _joueurAMarquer.transform.position.z) / 2f;
        Vector3 direction = Deplacement.Direction(automate.Joueur.transform.position, destination, automate.Joueur.Joueur.Vitesse);
        if (!automate.Joueur.Frappe)
            automate.Joueur.Rigidbody.velocity = direction;

    }

    public override Etat Suivant()
    {
        Automate_Poursuiveur automate = _automate as Automate_Poursuiveur;
        Etat res = this;

        //Si le gardien à le souafle
        if (Souafle.Possesseur != null)
        {
            Player possesseur = Souafle.Possesseur.GetComponent<Player>();
            if (possesseur.Joueur.Poste == Poste.GARDIEN)
            {
                //Son gardien a le souafle
                if (possesseur.Joueur.Club == automate.Joueur.Joueur.Club)
                {
                    res = new Etat_Poursuiveur_SeDemarquerGardien(automate);
                }
            }
            if (possesseur == automate.Joueur)
                res = new Etat_Poursuivre_Avancer(automate);
        }
        else
        {
            if (Math.Abs(Vector3.Distance(Souafle.Objet.transform.position, automate.Joueur.transform.position)) < 15)
                res = new Etat_Poursuivreur_AllerVersSouafle(automate);
        }

        return res;
    }
}