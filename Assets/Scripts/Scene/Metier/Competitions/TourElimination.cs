using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class TourElimination : Tour
{

    public TourElimination(string nom, DateTime heure, List<DateTime> dates, List<Decalage> decalages, bool allerRetour) : base(nom,heure,dates,decalages, allerRetour)
    {

    }

    public override void Initialiser(List<Club> clubs)
    {
        foreach (Club c in clubs)
            _clubs.Add(c);

        _matchs = GestionCalendrier.TirageAuSort(this);
    }

    public override List<Club> Qualifies()
    {
        List<Club> res = new List<Club>();

        foreach(Match m in _matchs)
        {
            res.Add(m.Vainqueur);
        }

        return res;
    }
}