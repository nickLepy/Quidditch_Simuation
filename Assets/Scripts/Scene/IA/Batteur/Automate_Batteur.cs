using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Automate_Batteur : IAutomate
{
    private Player _joueur;
    public Player Joueur { get { return _joueur; } }

    private Etat _courant;

    private Player _cible;
    public Player Cible { get { return _cible; } set { _cible = value; } }

    public Automate_Batteur(Player joueur)
    {
        _joueur = joueur;
        _courant = new Etat_Batteur_FrapperCognard(this) ;
        _cible = null;
    }

    public void Action()
    {
        if(_courant != null)
        {
            _courant.Action();
            _courant = _courant.Suivant();
        }
    }
}