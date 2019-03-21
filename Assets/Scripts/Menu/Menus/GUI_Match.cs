using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class GUI_Match : IGUI
{

    private Match _match;
    private Vector2 _scroll;

    public GUI_Match(Match match)
    {
        _match = match;
    }

    public IGUI GUI()
    {
        IGUI res = this;

        GUILayout.BeginArea(new Rect(Screen.width * 0.2f, Screen.height * 0.2f, Screen.width * 0.6f, Screen.height * 0.6f));

        GUILayout.BeginVertical();

        if (GUILayout.Button("Revenir", Styles.ButtonNormal(20, Styles.Texte2), GUILayout.Width(Screen.width * 0.1f)))
        {
            res = new GUI_AcceuilCompetition();
        }

        GUILayout.BeginHorizontal();

        GUILayout.Label(_match.Equipe1.Nom, Styles.LabelNormal(25, Styles.Texte2),GUILayout.Width(Screen.width*0.15f));
        GUILayout.Label(_match.Score1.ToString(), Styles.LabelNormal(25, Styles.Texte2), GUILayout.Width(Screen.width * 0.1f));
        GUILayout.Label(_match.Score2.ToString(), Styles.LabelNormal(25, Styles.Texte2), GUILayout.Width(Screen.width * 0.1f));
        GUILayout.Label(_match.Equipe2.Nom, Styles.LabelNormal(25, Styles.Texte2), GUILayout.Width(Screen.width * 0.15f));

        GUILayout.EndHorizontal();

        GUILayout.Space(Screen.height * 0.05f);
        GUILayout.Label("Possesion", Styles.LabelNormal(20, Styles.Texte2), GUILayout.Width(Screen.width * 0.5f));

        GUILayout.BeginHorizontal();
        GUILayout.Label((_match.Possession1 * 100).ToString("0.00") + " %", Styles.LabelNormal(20, Styles.Texte1), GUILayout.Width(Screen.width * 0.35f));
        GUILayout.Label((_match.Possession2 * 100).ToString("0.00") + " %", Styles.LabelNormal(20, Styles.Texte1), GUILayout.Width(Screen.width * 0.15f));
        GUILayout.EndHorizontal();

        GUILayout.Label("Tirs", Styles.LabelNormal(20, Styles.Texte2), GUILayout.Width(Screen.width * 0.5f));
        GUILayout.BeginHorizontal();
        GUILayout.Label(_match.Tirs1.ToString(), Styles.LabelNormal(20, Styles.Texte1), GUILayout.Width(Screen.width * 0.35f));
        GUILayout.Label(_match.Tirs2.ToString(), Styles.LabelNormal(20, Styles.Texte1), GUILayout.Width(Screen.width * 0.15f));
        GUILayout.EndHorizontal();

        _scroll = GUILayout.BeginScrollView(_scroll);

        foreach(EvenementMatch em in _match.Evenements)
        {
            GUILayout.BeginHorizontal();
            if (em.Club == _match.Equipe2)
                GUILayout.Label("", GUILayout.Width(Screen.width * 0.35f)); //Décalage à droite de l'écran
            GUILayout.Label(em.Temps + "e", Styles.LabelNormal(15, Styles.Texte2),GUILayout.Width(Screen.width*0.03f));
            GUILayout.Label(em.Joueur.Nom, Styles.LabelNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.1f));
            string nom = "(But)";
            if (em.Type == TypeEvenement.VIF_ATTRAPE) nom = "(Capture du vif)";
            GUILayout.Label(nom, Styles.LabelNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.1f));
            GUILayout.EndHorizontal();
        }
        
        GUILayout.EndScrollView();

        GUILayout.EndVertical();

        GUILayout.EndArea();

        return res;
    }
}