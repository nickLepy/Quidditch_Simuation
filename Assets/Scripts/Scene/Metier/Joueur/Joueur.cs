using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Joueur
{
    public string Prenom { get; set; }
    public string Nom { get; set; }
    public Poste Poste { get; set; }
    public int Vitesse { get; set; }
    public int Precision { get; set; }
    /// <summary>
    /// True : H
    /// False : F
    /// </summary>
    public bool Sexe { get; set; }

    public float Niveau
    {
        get
        {
            return (Vitesse + Precision) / 2.0f;
        }
    }

    public Club Club
    {
        get
        {
            Club res = null;
            foreach (Club c in Session.Instance.Partie.Gestionnaire.Clubs)
            {
                foreach (Joueur j in c.Joueurs)
                    if (j == this) res = c;
            }

            return res;
        }
    }
    
    

    public Joueur()
    {
    }
}
