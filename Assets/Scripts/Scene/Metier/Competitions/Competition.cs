using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Competition
{
    private int _id;
    private string _nom;
    private string _logo;
    private List<Tour> _tours;
    private int _tourActuel;
    private List<Article> _articles;
    private DateTime _debutSaison;

    public int ID { get { return _id; } }
    public string Nom { get { return _nom; } }
    public string Logo { get { return _logo; } }
    public List<Tour> Tours { get { return _tours; } }
    public int TourActuel { get { return _tourActuel; } set { _tourActuel = value; } }
    public List<Article> Articles { get { return _articles; } }
    public DateTime DebutSaison { get { return _debutSaison; } }

    public Competition(int id, string nom, string logo, DateTime debutSaison)
    {
        _id = id;
        _nom = nom;
        _logo = logo;
        _tours = new List<Tour>();
        _tourActuel = 0;
        _articles = new List<Article>();
        _debutSaison = debutSaison;
    }

    public void Initialiser()
    {
        _tours[0].Initialiser(new List<Club>());
    }

    public void TourSuivant()
    {
        if(_tours.Count > _tourActuel+1 )
        {
            _tourActuel++;
            _tours[_tourActuel].Initialiser(_tours[_tourActuel - 1].Qualifies());
        }   
    }

    public Dictionary<Joueur,int> Buteurs()
    {
        Dictionary<Joueur, int> res = new Dictionary<Joueur, int>();

        foreach(Tour t in _tours)
        {
            foreach(Match m in t.Matchs)
            {
                foreach(EvenementMatch em in m.Evenements)
                {
                    if(em.Type == TypeEvenement.BUT)
                    {
                        if (res.ContainsKey(em.Joueur))
                            res[em.Joueur] += 10;
                        else
                            res.Add(em.Joueur, 10);
                    }
                }
            }
        }
        return res;
    }

    public void RAZ()
    {

    }
}