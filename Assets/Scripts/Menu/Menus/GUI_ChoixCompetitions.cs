using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class GUI_ChoixCompetitions : IGUI
{

    private int _id;
    private int _idClub;
    private Competition _competition;
    private List<Club> _clubs;
    
    public GUI_ChoixCompetitions()
    {
        Session.Instance.Partie = new Partie(new Gestionnaire());
        ParseurXML parseur = new ParseurXML(Session.Instance.Partie.Gestionnaire);
        parseur.Charger();
        _id = 0;
        _idClub = 0;
        _clubs = new List<Club>();

        _competition = Session.Instance.Partie.Gestionnaire.Competitions[_id];
        _clubs = new List<Club>();
        foreach (Tour t in _competition.Tours) foreach (Club c in t.Clubs) _clubs.Add(c);

    }

    public IGUI GUI()
    {
        Gestionnaire gestionnaire = Session.Instance.Partie.Gestionnaire;
        IGUI res = this;

        GUILayout.BeginArea(new Rect(Screen.width * 0.2f, Screen.height * 0.2f, Screen.width * 0.6f, Screen.height * 0.6f));

        Competition competition = gestionnaire.Competitions[_id];

        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();

        //Competition

        if (GUILayout.Button(Styles.FlecheGauche(), GUILayout.Width(40), GUILayout.Height(40)))
        {
            if (_id > 0) _id--;
            _competition = gestionnaire.Competitions[_id];
            _clubs = new List<Club>();
            foreach (Tour t in _competition.Tours) foreach (Club c in t.Clubs) _clubs.Add(c);
        }

        GUILayout.BeginVertical();
        GUILayout.Label("Compétition", Styles.LabelNormal(30, Styles.Texte2), GUILayout.Width(Screen.width * 0.25f));
        GUILayout.Label(Resources.Load("Logos/" + competition.Logo) as Texture, GUILayout.Width(240), GUILayout.Height(240));
        GUILayout.Label(competition.Nom, Styles.LabelNormal(20, Styles.Texte1));
        GUILayout.EndVertical();

        if (GUILayout.Button(Styles.FlecheDroite(), GUILayout.Width(40), GUILayout.Height(40)))
        {
            if (_id < (gestionnaire.Competitions.Count - 1)) _id++;
            _competition = gestionnaire.Competitions[_id];
            _clubs = new List<Club>();
            foreach (Tour t in _competition.Tours) foreach (Club c in t.Clubs) _clubs.Add(c);
        }

        //Equipes


        if (GUILayout.Button(Styles.FlecheGauche(), GUILayout.Width(40), GUILayout.Height(40)))
        {
            if (_idClub > 0) _idClub--;
        }

        if (_clubs.Count > 0)
        {
            GUILayout.BeginVertical();
            GUILayout.Label("Club", Styles.LabelNormal(30, Styles.Texte2), GUILayout.Width(Screen.width * 0.25f));
            GUILayout.Label(Resources.Load("Logos/" + _clubs[_idClub].Logo) as Texture, GUILayout.Width(240), GUILayout.Height(240));
            GUILayout.Label(_clubs[_idClub].Nom, Styles.LabelNormal(20, Styles.Texte1));
            Styles.AfficherEtoiles(_clubs[_idClub]);
            GUILayout.EndVertical();
        }


        if (GUILayout.Button(Styles.FlecheDroite(), GUILayout.Width(40), GUILayout.Height(40)))
        {
            if (_idClub < (_clubs.Count - 1)) _idClub++;


        }

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Commencer", Styles.ButtonNormal(20, Styles.Texte2), GUILayout.Width(Screen.width * 0.2f)))
        {
            Session.Instance.Partie.Club = _clubs[_idClub];
            res = new GUI_AcceuilCompetition();
        }

        if (GUILayout.Button("Revenir", Styles.ButtonNormal(20, Styles.Texte2), GUILayout.Width(Screen.width * 0.2f)))
        {
            res = new GUI_MenuPrincipal();
        }
        GUILayout.EndHorizontal();

        GUILayout.EndHorizontal();

        

        GUILayout.EndVertical();

        GUILayout.EndArea();

        return res;
    }
}