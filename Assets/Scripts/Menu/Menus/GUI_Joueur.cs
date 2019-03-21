using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class GUI_Joueur : IGUI
{

    private Joueur _joueur;

    public GUI_Joueur(Joueur joueur)
    {
        _joueur = joueur;
    }

    public IGUI GUI()
    {
        IGUI res = this;

        GUILayout.BeginArea(new Rect(Screen.width * 0.2f, Screen.height * 0.2f, Screen.width * 0.6f, Screen.height * 0.6f));

        GUILayout.BeginVertical();

        GUILayout.Label(_joueur.Prenom + " " + _joueur.Nom, Styles.LabelNormal(25, Styles.Texte1));

        

        if(_joueur.Club != null)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(_joueur.Club.Nom, Styles.LabelNormal(18, Styles.Texte2));
            Styles.AfficherLogo(_joueur.Club.Logo, 30, Screen.width * 0.05f);
            GUILayout.EndHorizontal();
        }
        

        GUILayout.Label("Poste : " + _joueur.Poste.ToString(), Styles.LabelNormal(18, Styles.Texte2));
        GUILayout.Label("Précision : " + _joueur.Precision.ToString(), Styles.LabelNormal(18, Styles.Texte2));
        GUILayout.Label("Vitesse : " + _joueur.Vitesse.ToString(), Styles.LabelNormal(18, Styles.Texte2));

        if (GUILayout.Button("Revenir", Styles.ButtonNormal(20, Styles.Texte2), GUILayout.Width(Screen.width * 0.2f)))
        {
            res = new GUI_AcceuilCompetition();
        }

        GUILayout.EndVertical();

        GUILayout.EndArea();

        return res;
    }
}