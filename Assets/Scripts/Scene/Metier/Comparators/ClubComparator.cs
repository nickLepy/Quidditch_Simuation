using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class ClubComparator : IComparer<Club>
{

    private IClassable _tour;
   
    public ClubComparator(IClassable t)
    {
        _tour = t;
    }

    public int Compare(Club x, Club y)
    {
        return Points(y) - Points(x);
    }

    private int Points(Club c)
    {
        return _tour.Points(c);
    }
}