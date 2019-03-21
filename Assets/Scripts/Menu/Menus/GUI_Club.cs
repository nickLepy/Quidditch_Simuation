using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class GUI_Club : IGUI
{

    private Club _club;

    public GUI_Club(Club club)
    {
        _club = club;
    }

    public IGUI GUI()
    {
        IGUI res = this;


        GUILayout.BeginArea(new Rect(Screen.width * 0.2f, Screen.height * 0.2f, Screen.width * 0.6f, Screen.height * 0.6f));


        GUILayout.BeginVertical();

        GUILayout.Label("Joueurs", Styles.LabelNormal(20, Styles.Texte1));

        GUILayout.Space(Screen.height * 0.03f);

        foreach(Joueur j in _club.Joueurs)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(j.Prenom, Styles.LabelNormal(15, Styles.Texte2),GUILayout.Width(Screen.width*0.1f));
            GUILayout.Label(j.Nom, Styles.LabelNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.1f));
            GUILayout.Label(j.Poste.ToString(), Styles.LabelNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.1f));
            GUILayout.Label(j.Niveau.ToString("0.0"), Styles.LabelNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.05f));
            GUILayout.EndHorizontal();
        }

        GUILayout.Space(Screen.height * 0.03f);

        if (GUILayout.Button("Revenir", Styles.ButtonNormal(20, Styles.Texte2),GUILayout.Width(Screen.width*0.2f)))
        {
            res = new GUI_AcceuilCompetition();
        }

        GUILayout.EndVertical();

        GUILayout.EndArea();

        return res;
    }
}