using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Article
{

    private string _titre;
    public string Titre { get { return _titre; } }
    private int _importance;
    public int Importance { get { return _importance; } }
    private DateTime _date;
    public DateTime Date { get { return _date; } }

    public Article(string titre, int importance, DateTime date)
    {
        _titre = titre;
        _importance = importance;
        _date = date;
    }

    public static Article Ecrire(Match match)
    {
        string titre = "";
        if((match.Temps.Minute + (match.Temps.Hour * 60)) < 30)
        {
            titre += (match.Temps.Minute + (match.Temps.Hour * 60)).ToString() + " minutes pour capturer le vif : ";
        }
        else if (match.Temps.Hour > 1)
        {
            titre += "Plus de " + match.Temps.Hour + " heures pour capturer le vif : ";
        }

        if (match.Vainqueur.Niveau < match.Perdant.Niveau)
            titre += "Exploit de " + match.Vainqueur.Nom + " qui battent " + match.Perdant.Nom;
        else
            titre += match.Vainqueur.Nom + " s'impose contre " + match.Perdant.Nom;
 
        int importance = 0;
        return new Article(titre, importance, Session.Instance.Partie.Date);
    }

    public override string ToString()
    {
        return _date.ToString("dd/MM/yy") + " : " + _titre;
    }

}