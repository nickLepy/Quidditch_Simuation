using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class TourChampionnat : Tour, IClassable
{

    private int _qualifies;

    public TourChampionnat(string nom, DateTime heure, List<DateTime> jours, bool allerRetour, List<Decalage> decalages, int qualifies) : base(nom,heure,jours, decalages, allerRetour)
    {
        _allerRetour = allerRetour;
        _qualifies = qualifies;
    }

    public override void Initialiser(List<Club> clubs)
    {
        foreach (Club c in clubs)
            _clubs.Add(c);

        _matchs = GestionCalendrier.GenererCalendrier(this.Clubs, this.DateMatchs, this.Heure, this.Decalages);
    }

    public override List<Club> Qualifies()
    {
        List<Club> classement = Classement();
        List<Club> qualifies = new List<Club>();
        for(int i = 0; i<_qualifies; i++)
        {
            qualifies.Add(classement[i]);
        }
        return classement;
    }

    public List<Club> Classement()
    {
        ClubComparator comparator = new ClubComparator(this);
        List<Club> classement = new List<Club>(_clubs);
        classement.Sort(comparator);
        return classement;
    }

    public int Points(Club c)
    {
        int points = 0;
        foreach(Match m in _matchs)
        {
            if(m.Termine)
            {
                if (m.Equipe1 == c)
                    points += m.Score1;
                else if (m.Equipe2 == c)
                    points += m.Score2;
            }
        }

        return points;
    }

    public int Joues(Club c)
    {
        int joues = 0;
        foreach(Match m in _matchs)
        {
            if (m.Termine && (m.Equipe1 == c || m.Equipe2 == c)) joues++;
        }
        return joues;
    }

    public List<Match> ProchaineJournee()
    {
        List<Match> res = new List<Match>();

        int indMatch = -1;
        DateTime plusTot = new DateTime(2000,1,1);
        int i = 0;
        foreach(Match m in _matchs)
        {
            if(!m.Termine && ( DateTime.Compare(m.Jour,plusTot) < 0 ||plusTot.Year == 2000))
            {
                plusTot = m.Jour;
                indMatch = i;
            }
            i++;
        }

        int total = MatchsParJournee();
        int journee = (indMatch / total);
        res = Journee(journee + 1); //+1 car journee va de 0 à n-1
        res.Sort(new MatchComparator());

        return res;
    }

    public int Journee(Match match)
    {
        return (_matchs.IndexOf(match) / MatchsParJournee()) + 1;
    }

    private int MatchsParJournee()
    {
        int total = _clubs.Count;
        //if (_clubs.Count % 2 == 1) total++;
        total /= 2;
        return total;
    }

    public List<Match> Journee(int journee)
    {
        List<Match> res = new List<Match>();
        int total = MatchsParJournee();
        int indice = journee - 1;
        for (int i = indice * total; i < (indice + 1) * total; i++)
        {
            res.Add(_matchs[i]);
        }

        return res;
    }
}