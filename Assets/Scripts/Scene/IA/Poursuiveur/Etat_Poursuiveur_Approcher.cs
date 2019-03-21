using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Etat_Poursuiveur_Approcher : Etat
{

    Vector3 _angle;

    public Etat_Poursuiveur_Approcher(IAutomate automate) : base(automate)
    {
        _angle = UnityEngine.Random.insideUnitSphere * 4;
        Debug.Log(_angle.x + " - " + _angle.y + " - " + _angle.z);
    }

    public override void Action()
    {
        Automate_Poursuiveur automate = _automate as Automate_Poursuiveur;
        if(Souafle.Possesseur!=null)
        {
            
            Vector3 dest = new Vector3(Souafle.Possesseur.transform.position.x + _angle.x, Souafle.Possesseur.transform.position.z + _angle.y, Souafle.Possesseur.transform.position.z + _angle.z);
            if(!automate.Joueur.Frappe)
                automate.Joueur.GetComponent<Rigidbody>().velocity = Deplacement.Direction(automate.Joueur.transform.position, dest, automate.Joueur.Joueur.Vitesse);
        }
    }

    public override Etat Suivant()
    {
        return this;
    }
}