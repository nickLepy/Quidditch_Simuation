using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class GUI_Options : IGUI
{

    private bool _toggle;

    public GUI_Options()
    {
        _toggle = Session.Instance.Partie.Options.SimulerMatchs;
    }

    public IGUI GUI()
    {
        IGUI res = this;

        GUILayout.BeginArea(new Rect(Screen.width * 0.2f, Screen.height * 0.2f, Screen.width * 0.6f, Screen.height * 0.6f));

        GUILayout.BeginVertical();
        if (GUILayout.Button("Revenir",Styles.ButtonNormal(20, Styles.Texte2),GUILayout.Width(Screen.width*0.1f)))
        {
            res = new GUI_AcceuilCompetition();
        }

        GUILayout.Space(Screen.height * 0.1f);

        _toggle = GUILayout.Toggle(_toggle, "Simuler les matchs", Styles.ToggleNormal(20, Styles.Texte1));

        GUILayout.Space(Screen.height * 0.05f);
        if (GUILayout.Button("Valider", Styles.ButtonNormal(20, Styles.Texte2), GUILayout.Width(Screen.width * 0.1f)))
        {
            Session.Instance.Partie.Options.SimulerMatchs = _toggle;
            res = new GUI_AcceuilCompetition();
        }
        GUILayout.EndVertical();

        GUILayout.EndArea();


        return res;
    }
}