using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Match
{

    private static Match _match;
    public static Match LeMatch { get { return _match; } set { _match = value; } } //Match en live en 3D

    private Club _equipe1;
    private Club _equipe2;
    private DateTime _jour;
    private DateTime _temps;
    private int _score1;
    private int _score2;
    private bool _vif;
    private bool _termine;
    private List<EvenementMatch> _evenements;

    //Stats
    private int _tirs1;
    private int _tirs2;
    private List<Joueur> _tirs;
    private int _possession1; //Possession de l'équipe 1 en secondes
    private int _possession2; //Possession de l'équipe 2 en secondes

    public Club Equipe1 { get { return _equipe1; } }
    public Club Equipe2 { get { return _equipe2; } }
    public DateTime Temps { get { return _temps; } set { _temps = value; } }
    public int Score1 { get { return _score1; } set { _score1 = value; } }
    public int Score2 { get { return _score2; } set { _score2 = value; } }
    public bool Vif { get { return _vif; } set { _vif = value; } }
    public bool Termine { get { return _termine; } set { _termine = value; } }
    public DateTime Jour { get { return _jour; } set { _jour = value; } }
    public List<EvenementMatch> Evenements { get { return _evenements; } }

    public Club Vainqueur
    {
        get
        {
            Club vainqueur = Equipe1;
            if (Score2 > Score1)
                vainqueur = Equipe2;
            return vainqueur;
        }
    }

    public Club Perdant
    {
        get
        {
            Club perdant = Equipe1;
            if (perdant == Vainqueur) perdant = Equipe2;
            return perdant;
        }
    }

    public int Tirs1
    {
        get
        {
            int tirs = 0;
            foreach(Joueur j in _tirs)
            {
                if (j.Club == Equipe1) tirs++;
            }
            return tirs;
        }
    }

    public int Tirs2
    {
        get
        {
            return _tirs.Count - Tirs1;
        }
    }

    public float Possession1
    {
        get
        {
            int secondes1 = _possession1;
            int secondes2 = _possession2;
            return secondes1 / (secondes1 + secondes2 + 0.0f);
        }
    }

    public float Possession2
    {
        get
        {
            return 1 - Possession1;
        }
    }

    public void AjouterPossession1(int secondes)
    {
        _possession1 += secondes;
    }

    public void AjouterPossession2(int secondes)
    {
        _possession2 += secondes;
    }


    public Match(Club equipe1, Club equipe2, DateTime jour)
    {
        this._equipe1 = equipe1;
        this._equipe2 = equipe2;
        _temps = new DateTime(2000, 1, 1, 0, 0, 0);
        _possession1 = 0;
        _possession2 = 0;
        _score1 = 0;
        _score2 = 0;
        _vif = false;
        _termine = false;
        _tirs1 = 0;
        _tirs2 = 0;
        _tirs = new List<Joueur>();
        _jour = jour;
        _evenements = new List<EvenementMatch>();
    }

    public void Tir(Joueur joueur)
    {
        _tirs.Add(joueur);
        if (joueur.Club == Equipe1) _tirs1++;
        else _tirs2++;
    }


    /// <summary>
    /// Simule le match et donne directement le résultat final
    /// </summary>
    public void Simuler()
    {
        float n1 = Equipe1.NiveauMoyen(Poste.POURSUIVEUR);
        float n2 = Equipe2.NiveauMoyen(Poste.POURSUIVEUR);
        int n1i = (int)(n1 * 10); //Niveau moyen poursuiveur equipe 1
        int n2i = (int)(n2 * 10); //Niveau moyen poursuiveur equipe 2
        int nV1 = (int)(Equipe1.NiveauMoyen(Poste.ATTRAPEUR));//Niveau attrapeur equipe 1
        int nV2 = (int)(Equipe2.NiveauMoyen(Poste.ATTRAPEUR));//Niveau attrapeur equipe 2
        int temps = 0;
        while (!_termine)
        {
            temps++;
            int rand = UnityEngine.Random.Range(0, n1i + n2i);
            if(rand < n1i) //Equipe 1
            {
                AjouterPossession1(60);
                if (Attaque(Equipe1))
                {
                    Score1 += 10;
                    Evenements.Add(new EvenementMatch(Equipe1, Buteur(Equipe1), temps, TypeEvenement.BUT));
                }   
            }
            else
            {
                AjouterPossession2(60);
                if (Attaque(Equipe2))
                {
                    Score2 += 10;
                    Evenements.Add(new EvenementMatch(Equipe2, Buteur(Equipe2), temps, TypeEvenement.BUT));
                }

            }
            int randVif = UnityEngine.Random.Range(0, 4500);
            if (randVif < nV1)
            {
                _termine = true;
                _score1 += 150;
                _vif = true;
                Evenements.Add(new EvenementMatch(Equipe1, Attrapeur(Equipe1), temps, TypeEvenement.VIF_ATTRAPE));

            }
            else if(randVif >= nV1 && randVif < nV2)
            {
                _termine = true;
                _score2 += 150;
                Evenements.Add(new EvenementMatch(Equipe2, Attrapeur(Equipe2), temps, TypeEvenement.VIF_ATTRAPE));
            }
        }
        Temps = Temps.AddMinutes(temps);
    }

    private Joueur Buteur(Club c)
    {
        List<Joueur> liste = new List<Joueur>();
        Joueur res = null;

        foreach (Joueur j in c.Joueurs)
            if (j.Poste == Poste.POURSUIVEUR)
                liste.Add(j);

        if (liste.Count > 0)
            res = liste[UnityEngine.Random.Range(0, liste.Count)];
        return res;
        
    }

    private Joueur Attrapeur(Club c)
    {
        Joueur res = null;

        foreach (Joueur j in c.Joueurs)
            if (j.Poste == Poste.ATTRAPEUR)
                res = j;

        return res;
    }

    private bool Attaque(Club c)
    {
        bool res = false;
        int niv = (int)(c.NiveauMoyen(Poste.POURSUIVEUR)*100);
        int nivGardien;
        if (c == Equipe1) nivGardien = (int)(Equipe2.NiveauMoyen(Poste.GARDIEN) * 100);
        else nivGardien = (int)(Equipe1.NiveauMoyen(Poste.GARDIEN) * 100);
        int rand = UnityEngine.Random.Range(0, niv + nivGardien);
        if (rand < niv) res = true;
        return res;
    }
}