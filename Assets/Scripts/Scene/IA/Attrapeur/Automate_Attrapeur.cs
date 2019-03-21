using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Automate_Attrapeur : IAutomate
{
    private Player _joueur;
    public Player Joueur { get { return _joueur; } }

    private Etat _courant;

    public Automate_Attrapeur(Player joueur)
    {
        _joueur = joueur;
        _courant = new Etat_Attrapeur_Suivre(this);
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