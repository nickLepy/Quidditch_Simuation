using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Etat_Poursuivreur_AllerVersSouafle : Etat
{

    

    public Etat_Poursuivreur_AllerVersSouafle(IAutomate automate) : base(automate)
    {
        Automate_Poursuiveur auto = automate as Automate_Poursuiveur;
        auto.Joueur.ConvoiteSouafle = true;
        
    }

    public override void Action()
    {
        
        Automate_Poursuiveur auto = base._automate as Automate_Poursuiveur;
        Vector3 direction = Deplacement.Direction(auto.Joueur.transform.position, Souafle.Objet.transform.position, auto.Joueur.Joueur.Vitesse);
        if (!auto.Joueur.Frappe)
            auto.Joueur.Rigidbody.velocity = direction;
        
    }

    public override Etat Suivant()
    {
        Automate_Poursuiveur automate = base._automate as Automate_Poursuiveur;
        Etat res = this;
        if (Souafle.Possesseur == automate.Joueur.gameObject)
            res = new Etat_Poursuivre_Avancer(automate);

        //Quelqu'un d'autre à le souafle
        else if(Souafle.Possesseur != null)
        {
            Player possesseur = Souafle.Possesseur.GetComponent<Player>();
            //Il est dans son équipe
            if (possesseur.Joueur.Club == automate.Joueur.Joueur.Club)
            {
                if (UnityEngine.Random.Range(0, 1) == 0) res = new Etat_Poursuiveur_SeDemarquer(automate);
                else res = new Etat_Poursuiveur_Approcher(automate);
            }
            //Il n'est pas dans son équipe
            else
            {
                res = new Etat_Poursuiveur_Marquage(automate);
                if(Math.Abs(Vector3.Distance(Souafle.Objet.transform.position,automate.Joueur.transform.position))<5)
                {
                    res = new Etat_Poursuivreur_AllerVersSouafle(automate);
                }

            }

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
        else //Pas encore de possesseur
        {
            if(Math.Abs(Vector3.Distance(automate.Joueur.transform.position,Souafle.Objet.transform.position)) > 5)
            {
                foreach(Player p in GameObject.FindObjectsOfType<Player>())
                {
                    //Si un poursuiveur de son équipe est proche du souafle, alors il se démarque préventivement
                    if (p.Joueur.Poste == Poste.POURSUIVEUR && p.Joueur.Club == automate.Joueur.Joueur.Club && Math.Abs(Vector3.Distance(p.transform.position,Souafle.Objet.transform.position))<5)
                    {
                        res = new Etat_Poursuiveur_SeDemarquer(automate);
                    }
                }
            }

        }
        
        
        return res;
    }
}
