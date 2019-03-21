using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class VueTourElimination : VueTour
{
    private TourElimination _tour;

    public VueTourElimination(TourElimination tour)
    {
        _tour = tour;
    }


    public List<Match> Calendrier()
    {
        List<Match> liste = new List<Match>(_tour.Matchs);
        liste.Sort(new MatchComparator());
        return liste;
    }

    public void Classement()
    {
        
    }

    public List<Match> ProchainsMatchs()
    {
        return _tour.ProchainsMatchs();
    }
}