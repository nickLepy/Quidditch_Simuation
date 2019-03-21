using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Club
{
    private string _nom;
    public string Nom { get { return _nom; } }
    private List<Joueur> _joueurs;
    public List<Joueur> Joueurs { get { return _joueurs; } }
    private Couleur _couleur;
    public Couleur Couleur { get { return _couleur; } }

    private int _id;
    public int ID { get { return _id; } }

    private int _formation;
    public int Formation { get { return _formation; } set { _formation = value; } }

    private string _logo;
    public string Logo { get { return _logo; } }

    public float Niveau
    {
        get
        {
            int joueurs = 0;
            float niv = 0;
            foreach (Joueur j in _joueurs)
            {
                joueurs++;
                niv += j.Niveau;
            }

            return niv / (joueurs + 0.0f);
        }
        
    }

    public float NiveauMoyen(Poste p)
    {
        float res = 0;
        int count = 0;
        foreach(Joueur j in _joueurs)
        {
            if(j.Poste == p)
            {
                count++;
                res += j.Niveau;
            }
        }

        return res / (count + 0.0f);
    }

    public Club(int id, string nom,Couleur couleur, string logo, int formation)
    {
        _id = id;
        _nom = nom;
        _joueurs = new List<Joueur>();
        _couleur = couleur;
        _logo = logo;
        _formation = formation;
    }

    public void GenererJoueur(Poste poste)
    {
        Joueur j = new Joueur();
        j.Prenom = "A";
        j.Nom = "A";
        j.Sexe = UnityEngine.Random.Range(0, 2) == 0 ? true : false;
        j.Poste = poste;
        j.Precision = UnityEngine.Random.Range(_formation - 6, _formation + 6);
        j.Vitesse = UnityEngine.Random.Range(_formation - 6, _formation + 6);
        _joueurs.Add(j);
    }
}
