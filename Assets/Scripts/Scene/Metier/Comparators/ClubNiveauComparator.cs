using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class ClubNiveauComparator : IComparer<Club>
{

    public int Compare(Club x, Club y)
    {
        return (int)(y.Niveau * 10 - x.Niveau * 10);
    }
}
