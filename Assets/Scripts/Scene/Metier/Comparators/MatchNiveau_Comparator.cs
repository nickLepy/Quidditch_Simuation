using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MatchNiveau_Comparator : IComparer<Match>
{
    public int Compare(Match x, Match y)
    {
        return (int)(((x.Equipe1.Niveau + x.Equipe2.Niveau) * 10) - ((y.Equipe1.Niveau + y.Equipe2.Niveau) * 10));
    }
}
