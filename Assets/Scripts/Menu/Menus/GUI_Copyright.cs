using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class GUI_Copyright : IGUI
{
    public IGUI GUI()
    {
        IGUI res = this;

        GUILayout.BeginArea(new Rect(Screen.width * 0.2f, Screen.height * 0.2f, Screen.width * 0.6f, Screen.height * 0.7f));

        GUILayout.BeginVertical();

        GUILayout.Label("Musiques", Styles.LabelNormal(20, Styles.Texte1));

        GUILayout.Space(Screen.height * 0.05f);

        GUILayout.Label("Epic Presentation by AShamaluevMusic (https://www.ashamaluevmusic.com)", Styles.LabelNormal(15, Styles.Texte2));
        GUILayout.Label("Cinematic Light by AShamaluevMusic  (https://www.ashamaluevmusic.com)", Styles.LabelNormal(15, Styles.Texte2));
        GUILayout.Label("Cinematic Dupstep by AShamaluevMusic (https://www.ashamaluevmusic.com)", Styles.LabelNormal(15, Styles.Texte2));
        GUILayout.Space(Screen.height * 0.1f);

        GUILayout.Label("Polices", Styles.LabelNormal(20, Styles.Texte1));
        GUILayout.Space(Screen.height * 0.05f);

        GUILayout.Label("HarryPotter7 (https://fr.fonts2u.com/harrypotter7.police)", Styles.LabelNormal(15, Styles.Texte2));



        if (GUILayout.Button("Retour",Styles.ButtonNormal(20,Styles.Texte1)))
        {
            res = new GUI_MenuPrincipal();
        }

        GUILayout.EndVertical();


        GUILayout.EndArea();

        return res;
    }
}