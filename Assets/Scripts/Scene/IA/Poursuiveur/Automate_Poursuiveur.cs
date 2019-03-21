using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Automate_Poursuiveur : IAutomate
{
    private Player _joueur;
    public Player Joueur { get { return _joueur; } }

    private Etat _courant;

    public Automate_Poursuiveur(Player joueur)
    {
        _joueur = joueur;
        _courant = new Etat_Poursuivreur_AllerVersSouafle(this);
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