using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Decalage
{
    private int _nombreMatchs;
    private int _decalageJours;
    private DateTime _heure;

    public int NombreMatchs { get { return _nombreMatchs; } }
    public int DecalageJours { get { return _decalageJours; } }
    public DateTime Heure { get { return _heure; } }

    public Decalage(int nbJour, int nbMatchs, DateTime heure)
    {
        _decalageJours = nbJour;
        _nombreMatchs = nbMatchs;
        _heure = heure;
    }
}