using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class VueTourPoules : VueTour
{

    private TourPoules _tour;
    private Vector2 _scroll;

    public VueTourPoules(TourPoules tour)
    {
        _tour = tour;
    }

    public List<Match> ProchainsMatchs()
    {
        return _tour.ProchaineJournee();
    }

    public List<Match> Calendrier()
    {
        List<Match> liste = _tour.Journee(GUI_AcceuilCompetition.Journee);
        liste.Sort(new MatchComparator());
        return liste;
    }

    public void Classement()
    {
        _scroll = GUILayout.BeginScrollView(_scroll);
        GUILayout.BeginVertical();
        GUILayout.Label("Classement", Styles.LabelNormal(20, Styles.Texte1), GUILayout.Width(Screen.width * 0.25f));
        GUILayout.BeginHorizontal();
        GUILayout.Label("Club", Styles.LabelNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.15f));
        GUILayout.Label("Pts", Styles.LabelNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.05f));
        GUILayout.Label("J", Styles.LabelNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.05f));
        GUILayout.EndHorizontal();

        for(int i = 0; i< _tour.NombrePoules; i++)
        {
            GUILayout.Label("Poule " + i, Styles.LabelNormal(18, Styles.Texte1));
            foreach (Club c in _tour.Classement(i))
            {
                GUILayout.BeginHorizontal();
                Styles.AfficherLogo(c.Logo, 30, 0.03f);
                GUILayout.Label(c.Nom, Styles.LabelNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.12f));
                GUILayout.Label(_tour.Points(c).ToString(), Styles.LabelNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.05f));
                GUILayout.Label(_tour.Joues(c).ToString(), Styles.LabelNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.05f));
                GUILayout.EndHorizontal();
            }
        }
        
        GUILayout.EndVertical();
        GUILayout.EndScrollView();
    }
}