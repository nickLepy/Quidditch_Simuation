using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_ChoixEquipes : IGUI
{
    private int _id1;
    private int _id2;

    public GUI_ChoixEquipes()
    {
        Session.Instance.Partie = new Partie(new Gestionnaire());
        ParseurXML parseur = new ParseurXML(Session.Instance.Partie.Gestionnaire);
        parseur.Charger();
        _id1 = 0;
        _id2 = 1;
    }

    public IGUI GUI()
    {
        Gestionnaire gestionnaire = Session.Instance.Partie.Gestionnaire;
        IGUI res = this;

        GUILayout.BeginArea(new Rect(Screen.width * 0.2f, Screen.height * 0.2f, Screen.width * 0.6f, Screen.height * 0.7f));

        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();

        Club dom = gestionnaire.Clubs[_id1];
        Club ext = gestionnaire.Clubs[_id2];

        
        if(GUILayout.Button(Styles.FlecheGauche(), GUILayout.Width(40), GUILayout.Height(40)))
        {
            if (_id1 > 0) _id1--;
        }
        GUILayout.BeginVertical();
        GUILayout.Label("Equipe à domicile", Styles.LabelNormal(30, Styles.Texte2));
        GUILayout.Label(Resources.Load("Logos/" + dom.Logo) as Texture, GUILayout.Width(240), GUILayout.Height(240));
        GUILayout.Label(dom.Nom, Styles.LabelNormal(20, Styles.Texte1));
        Styles.AfficherEtoiles(dom);
        GUILayout.EndVertical();
        if (GUILayout.Button(Styles.FlecheDroite(), GUILayout.Width(40), GUILayout.Height(40)))
        {
            if (_id1 < gestionnaire.Clubs.Count-1) _id1++;
        }
        if (GUILayout.Button(Styles.FlecheGauche(), GUILayout.Width(40), GUILayout.Height(40)))
        {
            if (_id2 > 0) _id2--;
        }
        GUILayout.BeginVertical();
        GUILayout.Label("Equipe à l'extérieur", Styles.LabelNormal(30, Styles.Texte2));
        GUILayout.Label(Resources.Load("Logos/" + ext.Logo) as Texture, GUILayout.Width(240), GUILayout.Height(240));
        GUILayout.Label(ext.Nom,Styles.LabelNormal(20, Styles.Texte1));
        Styles.AfficherEtoiles(ext);
        GUILayout.EndVertical();
        if (GUILayout.Button(Styles.FlecheDroite(), GUILayout.Width(40), GUILayout.Height(40)))
        {
            if (_id2 < gestionnaire.Clubs.Count - 1) _id2++;
        }

        if (GUILayout.Button("Commencer", Styles.ButtonNormal(40, Styles.Texte2)))
        {
            Match m = new Match(gestionnaire.Clubs[_id1], gestionnaire.Clubs[_id2], new DateTime(2018, 1, 1, 20, 0, 0));
            Match.LeMatch = m;
            SceneManager.LoadScene(1);
        }

        GUILayout.EndHorizontal();

       
        GUILayout.EndVertical();

        GUILayout.EndArea();

        return res;
    }
}