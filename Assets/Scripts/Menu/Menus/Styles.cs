using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Styles
{
    private static Font _fontHP = Resources.Load("Font/HarryPotter7", typeof(Font)) as Font;
    private static Font _fontNP = Resources.Load("Font/PathwayGothicOne-Regular", typeof(Font)) as Font;
    private static Texture2D[] _textures;

    public static GUIStyle LabelNormal(int fontSize, Color color)
    {
        GUIStyle style = new GUIStyle(UnityEngine.GUI.skin.label);
        style.font = _fontHP;
        style.fontSize = fontSize;
        style.normal.textColor = color;
        return style;
    }

    public static GUIStyle LabelNewspaper(int fontSize, Color color)
    {
        GUIStyle style = LabelNormal(fontSize, color);
        style.font = _fontNP;
        return style;
    }

    public static GUIStyle ToggleNormal(int fontSize, Color color)
    {
        GUIStyle style = new GUIStyle(UnityEngine.GUI.skin.toggle);
        style.font = _fontHP;
        style.fontSize = fontSize;
        style.normal.textColor = color;
        return style;
    }

    public static GUIStyle ScrollBar()
    {
        GUIStyle style = new GUIStyle(UnityEngine.GUI.skin.scrollView);

        style.normal.background = _textures[0];
        return style;
    }

    public static GUIStyle ButtonNormal(int fontSize, Color color)
    {
        GUIStyle style = new GUIStyle(UnityEngine.GUI.skin.button);
        style.font = _fontHP;
        style.fontSize = fontSize;
        style.normal.textColor = color;
        return style;
    }

    public static void AfficherLogo(string chemin, int taille, float largeur)
    {
        GUILayout.Label(Resources.Load("Logos/" + chemin) as Texture, GUILayout.Width(taille), GUILayout.Height(taille), GUILayout.Width(Screen.width*largeur));
    }

    public static string DateTime2Heure(DateTime date)
    {
        string res = date.Hour.ToString() ;
        if (date.Hour < 10)
            res += "0";
        res += ":" + date.Minute.ToString();
        if (date.Minute < 10)
            res += "0";
        return res;
    }

    private static void ChargerTextures()
    {
        _textures = new Texture2D[3];
        _textures[0] = Resources.Load("Textures/arrow", typeof(Texture2D)) as Texture2D;
        _textures[1] = Resources.Load("Textures/arrow2", typeof(Texture2D)) as Texture2D;
        _textures[2] = Resources.Load("Textures/star", typeof(Texture2D)) as Texture2D;
    }

    public static void AfficherEtoiles(Club c)
    {
        float niveau = c.Niveau;
        int etoiles = 0;
        if (niveau < 25) etoiles = 1;
        else if (niveau < 31) etoiles = 2;
        else if (niveau < 37) etoiles = 3;
        else if (niveau < 43) etoiles = 4;
        else etoiles = 5;
        GUILayout.BeginHorizontal();
        for (int i = 0; i < etoiles; i++)
        {
            GUILayout.Label(Etoile(), GUILayout.Width(30));
        }
        GUILayout.EndHorizontal();
    }

    public static Texture2D FlecheGauche()
    {
        if (_textures == null) ChargerTextures();
        return _textures[0];
    }

    public static Texture2D FlecheDroite()
    {
        if (_textures == null) ChargerTextures();
        return _textures[1];
    }

    public static Texture2D Etoile()
    {
        if (_textures == null) ChargerTextures();
        return _textures[2];
    }

    public static Color Texte1
    {
        get
        {
            return new Color(0.4f, 0.3f, 0);
        }
    }

    public static Color Texte2
    {
        get
        {
            return new Color(0.7f, 0.5f, 0.1f);
        }
    }


}
