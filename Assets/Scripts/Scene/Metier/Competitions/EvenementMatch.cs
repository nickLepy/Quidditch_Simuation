using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public enum TypeEvenement
{
    BUT,
    VIF_ATTRAPE
}

public class EvenementMatch
{

    private Joueur _joueur;
    private int _temps;
    private Club _club;
    private TypeEvenement _type;

    public Joueur Joueur { get { return _joueur; } }
    public int Temps { get { return _temps; } }
    public Club Club { get { return _club; } }
    public TypeEvenement Type { get { return _type; } }

    public EvenementMatch(Club club, Joueur joueur, int temps, TypeEvenement type)
    {
        _joueur = joueur;
        _club = club;
        _temps = temps;
        _type = type;
    }
    

}