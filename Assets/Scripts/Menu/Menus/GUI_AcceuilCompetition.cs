using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class GUI_AcceuilCompetition : IGUI
{
    private VueTour _vue;
    private Tour _tour;
    private Tour _previous;
    private static int _journee;
    private static int _index;
    public static int Journee { get { return _journee; } }
    public static int Index { get { return _index; } }
    private Vector2 _scroll, _scroll2;
    private static Competition _competition;
    public static Competition Competition { get { return _competition; } set { _competition = value; } }

    public GUI_AcceuilCompetition()
    {
        _tour = _competition.Tours[_competition.TourActuel];
        _vue = Vue(_tour);
        
        _journee = 1;
        _index = _competition.TourActuel;

    }

    private VueTour Vue(Tour t)
    {
        VueTour res = null;
        if (t as TourChampionnat != null)
            res = new VueTourChampionnat(t as TourChampionnat);
        else if (t as TourElimination != null)
            res = new VueTourElimination(t as TourElimination);
        else if (t as TourPoules != null)
            res = new VueTourPoules(t as TourPoules);
        return res;
    }

    public IGUI GUI()
    {
        _previous = _tour;
        _tour = _competition.Tours[_competition.TourActuel];
        if(_previous != _tour)
        {
            _vue = Vue(_tour);
        }

        IGUI res = this;

        GUILayout.BeginArea(new Rect(Screen.width * 0.05f, Screen.height * 0.1f, Screen.width * 0.9f, Screen.height * 0.8f));

        GUILayout.BeginVertical();

        GUILayout.Label(Session.Instance.Partie.Date.ToString("dd/MM/yy"), Styles.LabelNormal(20, Styles.Texte1), GUILayout.Width(Screen.width * 0.25f));

        #region BANDEAU_CENTRAL
        GUILayout.BeginHorizontal();


        //A gauche, classement ou matchs
        GUILayout.BeginVertical();
        _vue.Classement();

        //En bas à gauche, les buteurs

        if (GUILayout.Button("Avancer", Styles.ButtonNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.1f)))
        {
            while (!Session.Instance.Partie.Avancer()) ; //Avance jusqu'à la prochaine interrpution de partie
        }

        if (GUILayout.Button("Options", Styles.ButtonNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.1f)))
        {
            res = new GUI_Options();
        }

        GUILayout.Label("Buteurs", Styles.LabelNormal(20, Styles.Texte1), GUILayout.Width(Screen.width * 0.15f));

        _scroll2 = GUILayout.BeginScrollView(_scroll2);


        Dictionary<Joueur, int> buteurs = _competition.Buteurs();
        var item = from pair in buteurs orderby pair.Value descending select pair;
        int i = 0;
        foreach(KeyValuePair<Joueur,int> kvp in item)
        {
            if(i<10)
            {
                GUILayout.BeginHorizontal();
                if(GUILayout.Button(kvp.Key.Nom, Styles.LabelNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.1f)))
                {
                    res = new GUI_Joueur(kvp.Key);
                }
                Styles.AfficherLogo(kvp.Key.Club.Logo, 30, 0.03f);
                GUILayout.Label(kvp.Value.ToString(), Styles.LabelNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.05f));
                GUILayout.EndHorizontal();
            }
            
            i++;
        }

        GUILayout.EndScrollView();

       

        GUILayout.EndVertical();

        //Au centre : affichage des matchs à venir

        GUILayout.BeginVertical();

        List<Match> aVenir = _vue.ProchainsMatchs();

        aVenir.Sort(new MatchComparator());

        GUILayout.Label("Matchs à venir", Styles.LabelNormal(20, Styles.Texte1));
        if (aVenir.Count > 0)
        {
            res = AfficherMatchs(aVenir,res);
        }
        else
        {
            GUILayout.Label("Aucun match de prévu", Styles.LabelNormal(15, Styles.Texte2));
        }

        GUILayout.Space(Screen.height * 0.2f);

        //AU MILIEU EN BAS : CALENDRIER / RESULTATS
        Tour t = _competition.Tours[_index];
        VueTour vue = Vue(t);
        GUILayout.BeginHorizontal();
        List<Match> liste = new List<Match>();

        if (GUILayout.Button(Styles.FlecheGauche(), GUILayout.Width(40), GUILayout.Height(40)))
        {
            _journee = 1;
            if(_index > 0) _index--;
            
        }

        if (GUILayout.Button(Styles.FlecheGauche(), GUILayout.Width(40), GUILayout.Height(40)))
        {
            if (_journee > 1) _journee--;
        }

        GUILayout.Space(Screen.width * 0.3f);

        if (GUILayout.Button(Styles.FlecheDroite(), GUILayout.Width(40), GUILayout.Height(40)))
        {
            _journee++;

        }

        if (GUILayout.Button(Styles.FlecheDroite(), GUILayout.Width(40), GUILayout.Height(40)))
        {
            _journee = 1;
            if (_index + 1 < _competition.Tours.Count)
                _index++;
        }
        GUILayout.EndHorizontal();
        liste = vue.Calendrier();

        GUILayout.BeginVertical();
        GUILayout.Label("Calendrier complet", Styles.LabelNormal(20, Styles.Texte1));
        _scroll = GUILayout.BeginScrollView(_scroll);
        res = AfficherMatchs(liste, res);
        GUILayout.EndScrollView();
        GUILayout.EndVertical();
        




        GUILayout.EndVertical();

        // A DROITE : ARTICLES
        GUILayout.BeginVertical();

        GUILayout.Label("Depeches", Styles.LabelNormal(20, Styles.Texte1));

        List<Article> articles = new List<Article>(_competition.Articles);
        articles.Sort(new ArticleComparator());
        foreach(Article a in articles)
        {
            GUILayout.Label(a.ToString(), Styles.LabelNewspaper(18, Styles.Texte2));
        }

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
        #endregion
        
        

        GUILayout.EndVertical();

        GUILayout.EndArea();

        return res;
    }

    private IGUI AfficherMatchs(List<Match> matchs, IGUI gui)
    {
        IGUI res = gui;
        DateTime actuelle = new DateTime(2000, 1, 2);
        DateTime ancienne = new DateTime(2000, 1, 1);
        foreach (Match m in matchs)
        {
            actuelle = m.Jour;
            if (actuelle.Date != ancienne.Date)
            {
                GUILayout.Label("Le " + actuelle.Day + "/" + actuelle.Month + "/" + actuelle.Year, Styles.LabelNormal(15,Styles.Texte2));
            }
            GUILayout.BeginHorizontal();
            if(GUILayout.Button(m.Equipe1.Nom, Styles.LabelNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.1f)))
            {
                res = new GUI_Club(m.Equipe1);
            }
            Styles.AfficherLogo(m.Equipe1.Logo, 30, 0.03f);
            if (!m.Termine)
                GUILayout.Label(Styles.DateTime2Heure(m.Jour), Styles.LabelNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.05f));
            else
            {
                if (GUILayout.Button(m.Score1 + " - " + m.Score2, Styles.LabelNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.05f)))
                {
                    res = new GUI_Match(m);
                }
            }
                
            Styles.AfficherLogo(m.Equipe2.Logo, 30, 0.03f);
            if (GUILayout.Button(m.Equipe2.Nom, Styles.LabelNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.1f)))
            {
                res = new GUI_Club(m.Equipe2);
            }
            GUILayout.EndHorizontal();
            ancienne = actuelle;
        }
        return res;
    }
}