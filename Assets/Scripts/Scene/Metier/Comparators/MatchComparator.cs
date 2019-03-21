using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class MatchComparator : IComparer<Match>
{
    public int Compare(Match x, Match y)
    {
        return DateTime.Compare(x.Jour, y.Jour);
    }
}