using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Partie
{

    private DateTime _date;
    private Club _club;
    /// <summary>
    /// Si non null, on est en mode compétition simple et non carrière,
    /// et seule cette compétition est prise en compte dans les simulations, les autres sont ignorées
    /// </summary>
    private Competition _competition;
    private Gestionnaire _gestionnaire = null;
    private Options _options;

    public DateTime Date { get { return _date; } }
    public Club Club { get { return _club; } set { _club = value; } }
    public Gestionnaire Gestionnaire { get {
            return _gestionnaire; } }
    public Options Options { get { return _options; } }

    public Partie(Gestionnaire gestionnaire)
    {
        _date = new DateTime(2018, 7, 3);
        _club = null;
        _gestionnaire = gestionnaire;
        _options = new Options();
    }

    /// <summary>
    /// Avance d'un jour
    /// </summary>
    /// <returns>Retour vrai s'il est passé quelque chose d'important, faux sinon</returns>
    public bool Avancer()
    {
        bool res = false;
        _date = _date.AddDays(1);
        foreach(Competition competition in _gestionnaire.Competitions)
        {
            foreach(Tour t in competition.Tours)
            {
                if (t.JouerMatchs()) res = true;
                if (t.ProchainsMatchs().Count == 0 && competition.Tours.IndexOf(t) == competition.TourActuel)
                {
                    competition.TourSuivant();
                }
            }
        }
        if (_date.Day == 1) res = true;

        return res;
    }

}