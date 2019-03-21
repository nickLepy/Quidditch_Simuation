using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Utils
{

    public static Color Couleur2Color(Couleur couleur)
    {
        Color res = Color.red;
        switch (couleur)
        {
            case Couleur.BLEU:
                res = Color.blue;
                res.r = 0.3f;
                res.g = 0.3f;
                break;
            case Couleur.ROUGE:
                res = Color.red;
                res.g = 0.3f;
                res.b = 0.3f;
                break;
            case Couleur.VERT:
                res = Color.green;
                res.r = 0.3f;
                res.b = 0.3f;
                break;
            case Couleur.JAUNE:
                res = Color.yellow;
                res.b = 0.3f;
                break;
            case Couleur.BLANC:
                res = Color.white;
                break;
            case Couleur.VIOLET:
                res = new Color(0.72f, 0.33f, 0.82f);
                break;
            case Couleur.CYAN:
                res = Color.cyan;
                break;
            case Couleur.GRIS:
                res = Color.gray;
                break;
            case Couleur.ORANGE:
                res = new Color(1, 0.5f, 0);
                break;
        }
        return res;
    }

}