using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Etat_Gardien_Surveiller : Etat
{

    public Etat_Gardien_Surveiller(IAutomate automate) : base(automate)
    {
        Automate_Gardien a = _automate as Automate_Gardien;
        a.Joueur.ConvoiteSouafle = true;
    }

    public override void Action()
    {
        Automate_Gardien automate = base._automate as Automate_Gardien;
        automate.Joueur.transform.LookAt(Souafle.Objet.transform); //Regarde le souafle

        Vector3 dest = Souafle.Objet.transform.position;
        dest.z = automate.Joueur.transform.position.z;
        Vector3 direction = Deplacement.Direction(automate.Joueur.transform.position, dest, automate.Joueur.Joueur.Vitesse);
        automate.Joueur.Rigidbody.velocity = direction;

    }

    public override Etat Suivant()
    {
        Automate_Gardien automate = base._automate as Automate_Gardien;
        Etat res = this;
        if (Souafle.Objet.transform.parent == automate.Joueur.transform)
            res = new Etat_Gardien_Relancer(automate);
        return res;
    }
}