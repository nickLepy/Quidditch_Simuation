using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Automate_Gardien : IAutomate
{
    private Player _joueur;
    public Player Joueur { get { return _joueur; } }

    private Etat _courant;

    public Automate_Gardien(Player joueur)
    {
        _joueur = joueur;
        _courant = new Etat_Gardien_Surveiller(this);
        Vector3 position = Deplacement.CagesEquipe(Match.LeMatch, joueur.Joueur.Club).transform.position; //Sa cage
        if (position.z < 0) //Ne pas mettre le gardien sur la cage mais un peu devant
            position.z += 2;
        else
            position.z -= 2;
        _joueur.transform.position = new Vector3(position.x, 14, position.z); //Le -position.z renvoi la cage opposée, celle que le gardien défend
        
    }

    public void Action()
    {
        if (_courant != null)
        {
            _courant.Action();
            _courant = _courant.Suivant();
        }
    }
}