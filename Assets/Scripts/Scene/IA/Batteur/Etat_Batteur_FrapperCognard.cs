using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Etat_Batteur_FrapperCognard : Etat
{

    public Etat_Batteur_FrapperCognard(IAutomate ia) : base(ia)
    {
        (_automate as Automate_Batteur).Joueur.ConvoiteCognard = true;
    }

    private void TrouverCible()
    {
        Automate_Batteur automate = _automate as Automate_Batteur;
        automate.Cible = null;
        List<Player> cibles = new List<Player>();
        foreach (Player p in GameObject.FindObjectsOfType<Player>())
        {
            if (p.Joueur.Club != automate.Joueur.Joueur.Club && p.Joueur.Poste != Poste.GARDIEN)
            {
                if (p.transform == Souafle.Possesseur)
                    automate.Cible = p;
                cibles.Add(p);
            }
        }
        if (automate.Cible == null && cibles.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, cibles.Count - 1);
            automate.Cible = cibles[index];
        }
    }

    public override void Action()
    {
        
        Automate_Batteur automate = _automate as Automate_Batteur;
        if (automate.Cible == null) TrouverCible();
        else automate.Joueur.GetComponent<Rigidbody>().velocity = Deplacement.Direction(automate.Joueur.transform.position, Cognard.Objet.transform.position, automate.Joueur.Joueur.Vitesse);
    }

    public override Etat Suivant()
    {
        Automate_Batteur automate = _automate as Automate_Batteur;
        Etat res = this;
        if (!automate.Joueur.ConvoiteCognard) res = new Etat_Batteur_FrapperCognard(automate);
        return res;
    }
}