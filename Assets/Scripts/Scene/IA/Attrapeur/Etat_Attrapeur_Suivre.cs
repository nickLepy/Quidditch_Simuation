using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Etat_Attrapeur_Suivre : Etat
{

    public Etat_Attrapeur_Suivre(IAutomate automate) : base(automate)
    {

    }

    public override void Action()
    {
        Automate_Attrapeur aa = base._automate as Automate_Attrapeur;
        Vector3 direction = Deplacement.Direction(aa.Joueur.transform.position,Vif.Objet.transform.position, aa.Joueur.Joueur.Vitesse);
        if (!aa.Joueur.Frappe)
            aa.Joueur.Rigidbody.velocity = direction;

        /*
        if(Math.Abs(Vector3.Distance(aa.Joueur.transform.position,Vif.Objet.transform.position)) < 0.2f)
        {
            if (Match.LeMatch.Equipe1 == aa.Joueur.Joueur.Club) Match.LeMatch.Score1 += 150;
            else Match.LeMatch.Score2 += 150;
            GameObject.Find("Main Camera").GetComponent<Main>().SendMessage("FinMatch");
        }*/
    }

    public override Etat Suivant()
    {
        return this;
    }
}