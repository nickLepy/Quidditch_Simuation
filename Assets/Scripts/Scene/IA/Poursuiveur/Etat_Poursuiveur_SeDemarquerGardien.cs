using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Se démarquer pour permettre au gardien de relancer le souafle
/// </summary>
public class Etat_Poursuiveur_SeDemarquerGardien : Etat
{

    private Vector3 _direction;

    public Etat_Poursuiveur_SeDemarquerGardien(IAutomate automate) : base(automate)
    {
        (automate as Automate_Poursuiveur).Joueur.ConvoiteSouafle = true;


        int y = UnityEngine.Random.Range(10, 16); //Hauteur
        int x = UnityEngine.Random.Range(Constantes3D.BORDURE_GAUCHE, Constantes3D.BORDURE_DROITE); //Largeur
        int z = UnityEngine.Random.Range(10, 50); //Profondeur
        GameObject cages = Deplacement.CagesEquipe(Match.LeMatch, (automate as Automate_Poursuiveur).Joueur.Joueur.Club);
        if (cages.transform.position.z < 0)
            z = -z;
        _direction = new Vector3(x, y, z);
    }

    public override void Action()
    {
        Automate_Poursuiveur automate = _automate as Automate_Poursuiveur;
        Vector3 direction = Deplacement.Direction(automate.Joueur.transform.position, _direction, automate.Joueur.Joueur.Vitesse);
        if (!automate.Joueur.Frappe)
            automate.Joueur.Rigidbody.velocity = direction;

    }

    public override Etat Suivant()
    {
        Automate_Poursuiveur automate = _automate as Automate_Poursuiveur;
        Etat res = this;
        if(Souafle.Possesseur == null)
        {
            if (Math.Abs(Vector3.Distance(Souafle.Objet.transform.position, automate.Joueur.transform.position)) < 10)
                res = new Etat_Poursuivreur_AllerVersSouafle(automate);
        }
        else
        {
            Player p = Souafle.Possesseur.GetComponent<Player>();
            if (p.Joueur.Poste != Poste.GARDIEN && p.Joueur.Club == automate.Joueur.Joueur.Club)
                res = new Etat_Poursuiveur_SeDemarquer(automate);
            if (p == automate.Joueur)
                res = new Etat_Poursuivre_Avancer(automate);
        }
        

        return res;
    }
}