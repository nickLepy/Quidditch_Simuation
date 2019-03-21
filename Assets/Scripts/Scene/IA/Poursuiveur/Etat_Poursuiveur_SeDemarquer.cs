using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Poursuiveur tente de se placer vers la cage
/// </summary>
public class Etat_Poursuiveur_SeDemarquer : Etat
{
    private Vector3 _destination;

    public Etat_Poursuiveur_SeDemarquer(IAutomate automate) : base(automate)
    {
        _destination = Vector3.zero;
        Automate_Poursuiveur ap = base._automate as Automate_Poursuiveur;
        ap.Joueur.ConvoiteSouafle = true;
        GameObject cage = Deplacement.Cages(Match.LeMatch, ap.Joueur.Joueur.Club);
        _destination.z = cage.transform.position.z / UnityEngine.Random.Range(1.5f,2.5f);
        _destination.x = UnityEngine.Random.Range(Constantes3D.BORDURE_GAUCHE, Constantes3D.BORDURE_DROITE);
        _destination.y = UnityEngine.Random.Range(12, 16);
    }

    public override void Action()
    {
        Automate_Poursuiveur auto = base._automate as Automate_Poursuiveur;
        Vector3 direction = Deplacement.Direction(auto.Joueur.transform.position, _destination, auto.Joueur.Joueur.Vitesse);
        if (!auto.Joueur.Frappe)
            auto.Joueur.Rigidbody.velocity = direction;

    }

    public override Etat Suivant()
    {
        Automate_Poursuiveur automate = _automate as Automate_Poursuiveur;

        Etat res = this;
        //Vérifier qu'on a toujours le ballon

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
                //Le gardien adverse a le souafle
                else
                {
                    res = new Etat_Poursuiveur_Marquage(automate);
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