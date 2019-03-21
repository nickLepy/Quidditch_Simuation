using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Tour
{

    protected string _nom;
    protected List<Club> _clubs;
    protected List<Match> _matchs;
    protected DateTime _heure;
    protected List<DateTime> _dateMatchs;
    protected List<Decalage> _decalages;
    protected bool _allerRetour;

    public string Nom { get { return _nom; } }
    public List<Club> Clubs { get { return _clubs; } }
    public List<Match> Matchs { get { return _matchs; } }
    public List<DateTime> DateMatchs { get { return _dateMatchs; } }
    public DateTime Heure { get { return _heure; } }
    public List<Decalage> Decalages { get { return _decalages; } }
    public bool AllerRetour { get { return _allerRetour; } }
    public Competition Competition
    {
        get
        {
            Competition res = null;
            foreach (Competition comp in Session.Instance.Partie.Gestionnaire.Competitions)
                foreach (Tour t in comp.Tours)
                    if (t == this) res = comp;
            return res;
        }
    }

    public Tour(string nom, DateTime heure, List<DateTime> dates, List<Decalage> decalages, bool allerRetour)
    {
        _nom = nom;
        _clubs = new List<Club>();
        _matchs = new List<Match>();
        _heure = heure;
        _dateMatchs = new List<DateTime>(dates);
        _decalages = new List<Decalage>(decalages);
        _allerRetour = allerRetour;
    }

    /// <summary>
    /// Joue les matchs du jour
    /// </summary>
    /// <returns>Vrai si au moins un match a été joué, faux sinon</returns>
    public bool JouerMatchs()
    {
        Match aJouer = null;
        bool res = false;
        foreach(Match m in _matchs)
        {
            if (Session.Instance.Partie.Date.Date == m.Jour.Date)
            {
                res = true;
                if((m.Equipe1 == Session.Instance.Partie.Club || m.Equipe2 == Session.Instance.Partie.Club) && !Session.Instance.Partie.Options.SimulerMatchs)
                {
                    aJouer = m;
                }
                else
                {
                    m.Simuler();
                    Competition.Articles.Add(Article.Ecrire(m));
                }
                    
                
            }
        }

        if(aJouer != null)
        {
            Match.LeMatch = aJouer;
            SceneManager.LoadScene(1);
        }
        return res;
    }

    /*
    /// <summary>
    /// Joue les prochains matchs dans la date
    /// </summary>
    /// <returns>Renvoie vrai s'il n'y a plus de matchs à jouer, faux sinon</returns>
    public bool JouerProchainsMatchs()
    {
        bool termine = false;
        List<Match> prochainsMatchs = ProchainsMatchs();
        if (prochainsMatchs.Count == 0) termine = true;
        foreach(Match m in prochainsMatchs)
        {
            m.Simuler();
        }
        return termine;
    }*/

    /// <summary>
    /// Renvoi la liste des prochains matchs à se jouer selon la date
    /// </summary>
    /// <returns></returns>
    public List<Match> ProchainsMatchs()
    {
        List<Match> res = new List<Match>();
        bool continuer = true;
        DateTime date = new DateTime(2000, 1, 1);
        int i = 0;
        if (_matchs.Count == 0) continuer = false;
        while (continuer)
        {
            Match m = _matchs[i];
            if (!m.Termine)
            {
                if (date.Year == 2000)
                {
                    date = m.Jour;
                    res.Add(m);
                }
                else if (date.Date == m.Jour.Date)
                    res.Add(m);
                else continuer = false;
            }
            if (i == _matchs.Count - 1) continuer = false;
            i++;
        }


        return res;
    }

    /// <summary>
    /// Liste des clubs qualifiés
    /// </summary>
    /// <returns></returns>
    public abstract List<Club> Qualifies();
    /// <summary>
    /// Initialiser le tour
    /// </summary>
    /// <param name="clubs"></param>
    public abstract void Initialiser(List<Club> clubs);
    
}