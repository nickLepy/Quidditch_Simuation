using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Etat_Gardien_Relancer : Etat
{

    private int _temps = 0;
    
    private void JouerSon()
    {
        Camera.main.gameObject.GetComponent<Main>().Audio.AjouterSon(Son.LRG_STAD_SIFFLET2, 0.7f, 0.9f, false, 5, false, 0);

    }

    public Etat_Gardien_Relancer(IAutomate automate) : base(automate)
    {
        Automate_Gardien a = _automate as Automate_Gardien;
        a.Joueur.ConvoiteSouafle = false;

        a.Joueur.GetComponent<Rigidbody>().velocity = Vector3.zero;
        if(a.Joueur.Joueur.Club == Match.LeMatch.Equipe2)
            JouerSon();
    }

    public override void Action()
    {
        Automate_Gardien automate = _automate as Automate_Gardien;
        _temps++;
        if (_temps == 45)
        {
            //Relance
            automate.Joueur.ConvoiteSouafle = true;
            Player cible = null;
            float minDist = -1;
            foreach(Player p in GameObject.FindObjectsOfType<Player>())
            {
                if(p.Joueur.Poste == Poste.POURSUIVEUR && p.Joueur.Club == automate.Joueur.Joueur.Club)
                {
                    //Passe au plus près, il faudrait que ça soit au plus démarqué
                    float dist = Math.Abs(Vector3.Distance(automate.Joueur.transform.position, p.transform.position));
                    if(cible == null || dist < minDist)
                    {
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
        Automate_Gardien automate = _automate as Automate_Gardien;
        Etat res = this;
        if (Souafle.Possesseur != automate.Joueur.gameObject)
        {
            res = new Etat_Gardien_Surveiller(automate);
        }
        return res;
    }
}