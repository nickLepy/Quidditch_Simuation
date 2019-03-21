using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class GUI_MenuPrincipal : IGUI
{

    private float _ecart;

    private void JouerSon()
    {
        bool res = true;
        foreach(AudioSource a in GameObject.FindObjectsOfType<AudioSource>())
        {
            if (a.isPlaying) res = false;
        }
        if(res)
        {
            Audio a = new Audio(Camera.main.gameObject);
            a.AjouterSon(Son.MENU, 1, 0, false, 0, false, 0);
        }
    }

    public GUI_MenuPrincipal()
    {
        JouerSon();
    }

    public IGUI GUI()
    {
        IGUI res = this;
        _ecart = 0.05f;
        GUILayout.BeginArea(new Rect(Screen.width * 0.3f, Screen.height * 0.2f, Screen.width * 0.3f, Screen.height * 0.6f));

        GUILayout.BeginVertical();

        GUILayout.Label("Quidditch 2019",Styles.LabelNormal(45, new Color(0.3f, 0.2f, 0)));

        GUILayout.Space(Screen.height * 0.075f);

        if (GUILayout.Button("Match rapide", Styles.ButtonNormal(30, Styles.Texte2), GUILayout.Width(Screen.width*0.2f)))
        {
            res = new GUI_ChoixEquipes();
        }

        GUILayout.Space(Screen.height * _ecart);

        if (GUILayout.Button("Compétition", Styles.ButtonNormal(30, Styles.Texte2), GUILayout.Width(Screen.width * 0.2f)))
        {
            res = new GUI_ChoixCompetitions();
        }

        GUILayout.Space(Screen.height * _ecart);

        if (GUILayout.Button("Carrière", Styles.ButtonNormal(30, Styles.Texte2), GUILayout.Width(Screen.width * 0.2f)))
        {
            res = new GUI_ChoixCompetitions();
        }

        GUILayout.Space(Screen.height * _ecart*4);

        if (GUILayout.Button("Credits & Copyrights", Styles.ButtonNormal(15, Styles.Texte2), GUILayout.Width(Screen.width * 0.15f)))
        {
            res = new GUI_Copyright();
        }

        GUILayout.EndVertical();

        GUILayout.EndArea();

        return res;
    }
}