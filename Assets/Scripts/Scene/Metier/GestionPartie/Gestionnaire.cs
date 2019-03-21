using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Gestionnaire
{

    private List<Club> _clubs;
    private List<Competition> _competitions;
    private List<Competition> _anciennesCompetitions;
    
    public List<Club> Clubs { get { return _clubs; } }
    public List<Competition> Competitions { get { return _competitions; } }
    public List<Competition> AnciennesCompetitions { get { return _anciennesCompetitions; } }

    public Gestionnaire()
    {
        _clubs = new List<Club>();
        _competitions = new List<Competition>();
        _anciennesCompetitions = new List<Competition>();
    }

    public Club ObtenirClub(int id)
    {
        Club res = null;
        foreach(Club c in _clubs)
        {
            if (c.ID == id) res = c;
        }
        return res;
    }
    
    public void ArchiverCompetition(Competition competition)
    {
        _competitions.Remove(competition);
        _anciennesCompetitions.Add(competition);
    }

    

}