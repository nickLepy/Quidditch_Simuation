using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

public class ParseurXML
{
    private Gestionnaire _gestionnaire;

    public ParseurXML(Gestionnaire gestionnaire)
    {
        _gestionnaire = gestionnaire;
    }

    public void Charger()
    {
        ChargerClubs();
        ChargerJoueurs();
        ChargerCompetitions();
    }

    private void ChargerClubs()
    {
        XDocument doc = XDocument.Load("Database/clubs.xml");
        foreach (XElement e in doc.Descendants("Clubs"))
        {
            foreach (XElement e2 in e.Descendants("Club"))
            {
                int id = int.Parse(e2.Attribute("id").Value);
                string nom = e2.Attribute("nom").Value;
                Couleur couleur = Couleur.ROUGE;
                switch (e2.Attribute("couleur").Value)
                {
                    case "jaune": couleur = Couleur.JAUNE; break;
                    case "vert": couleur = Couleur.VERT; break;
                    case "rouge": couleur = Couleur.ROUGE; break;
                    case "bleu": couleur = Couleur.BLEU; break;
                    case "blanc": couleur = Couleur.BLANC; break;
                    case "cyan": couleur = Couleur.CYAN; break;
                    case "violet": couleur = Couleur.VIOLET; break;
                    case "gris": couleur = Couleur.GRIS; break;
                    case "orange": couleur = Couleur.ORANGE; break;
                }
                string logo = e2.Attribute("logo").Value;
                int formation = 0;
                if (e2.Attribute("formation") != null)
                    formation = int.Parse(e2.Attribute("formation").Value);
                _gestionnaire.Clubs.Add(new Club(id, nom, couleur, logo,formation));
            }
        }
    }

    private void ChargerCompetitions()
    {
        XDocument doc = XDocument.Load("Database/competitions.xml");
        foreach (XElement e in doc.Descendants("Competitions"))
        {
            foreach (XElement e2 in e.Descendants("Competition"))
            {
                int id = int.Parse(e2.Attribute("id").Value);
                string nom = e2.Attribute("nom").Value;
                string logo = e2.Attribute("logo").Value;
                string[] maj = e2.Attribute("miseAJour").Value.Split('/');
                DateTime miseAJour = new DateTime(2000, int.Parse(maj[1]), int.Parse(maj[0]));
                Competition competition = new Competition(id, nom, logo, miseAJour);
                Session.Instance.Partie.Gestionnaire.Competitions.Add(competition);
                foreach (XElement e3 in e2.Descendants("Tour"))
                {
                    string nomTour = e3.Attribute("nom").Value;
                    bool allerRetour = e3.Attribute("allerretour").Value == "O" ? true : false;
                    string[] heureStr = (e3.Attribute("heure").Value).Split(':');
                    DateTime heure = new DateTime(2000, 1, 1, int.Parse(heureStr[0]), int.Parse(heureStr[1]),0);

                    List<DateTime> dates = new List<DateTime>();
                    bool continuer = true;
                    int i = 1;
                    while(continuer)
                    {
                        try
                        {
                            string[] date = e3.Attribute("d" + i.ToString()).Value.Split('/');
                            DateTime d = new DateTime(2000,int.Parse(date[1]),int.Parse(date[0]));
                            dates.Add(d);
                            i++;
                        }
                        catch
                        {
                            continuer = false;
                        }
                    }

                    List<Decalage> decalages = new List<Decalage>();
                    foreach(XElement e4 in e3.Descendants("Decalage"))
                    {
                        int decalage = int.Parse(e4.Attribute("jour").Value);
                        int nbMatchs = int.Parse(e4.Attribute("nbMatchs").Value);
                        heureStr = (e4.Attribute("heure").Value).Split(':');
                        DateTime heureD = new DateTime(2000, 1, 1, int.Parse(heureStr[0]), int.Parse(heureStr[1]), 0);
                        decalages.Add(new Decalage(decalage, nbMatchs, heureD));
                    }
                    int qualifies;
                    Tour t;
                    switch(e3.Attribute("type").Value)
                    {
                        case "CH":
                            qualifies = int.Parse(e3.Attribute("qualifies").Value);
                            t = new TourChampionnat(nomTour, heure, dates, allerRetour, decalages, qualifies); break;
                        case "E" : t = new TourElimination(nomTour,heure,dates,decalages,allerRetour); break;
                        default:
                            int poules = int.Parse(e3.Attribute("nombrePoules").Value);
                            qualifies = int.Parse(e3.Attribute("qualifies").Value);

                            t = new TourPoules(nomTour, heure, dates, decalages,poules, allerRetour, qualifies);
                            break;
                    }
                    competition.Tours.Add(t);
                    foreach(XElement e4 in e3.Descendants("Participant"))
                    {
                        Club c = Session.Instance.Partie.Gestionnaire.ObtenirClub(int.Parse(e4.Attribute("id").Value));
                        t.Clubs.Add(c);
                    }
                }
            }
        }
    }

    private void ChargerJoueurs()
    {
        XDocument doc = XDocument.Load("Database/joueurs.xml");
        foreach (XElement e in doc.Descendants("Joueurs"))
        {
            foreach (XElement e2 in e.Descendants("Joueur"))
            {
                int idclub = int.Parse(e2.Attribute("club").Value);
                string nom = e2.Attribute("nom").Value;
                string prenom = e2.Attribute("prenom").Value;
                bool sexe = e2.Attribute("sexe").Value == "M" ? true : false;
                Poste poste = Poste.POURSUIVEUR;
                switch (e2.Attribute("poste").Value)
                {
                    case "P": poste = Poste.POURSUIVEUR; break;
                    case "B": poste = Poste.BATTEUR; break;
                    case "A": poste = Poste.ATTRAPEUR; break;
                    case "G": poste = Poste.GARDIEN; break;
                }
                int vitesse = int.Parse(e2.Attribute("vitesse").Value);
                int precision = int.Parse(e2.Attribute("precision").Value);
                _gestionnaire.ObtenirClub(idclub).Joueurs.Add(new Joueur
                {
                    Prenom = prenom,
                    Nom = nom,
                    Sexe = sexe,
                    Poste = poste,
                    Vitesse = vitesse,
                    Precision = precision
                });
            }
        }

        foreach(Club c in _gestionnaire.Clubs)
        {
            if(c.Joueurs.Count < 7)
            {
                c.GenererJoueur(Poste.GARDIEN);
                c.GenererJoueur(Poste.ATTRAPEUR);
                c.GenererJoueur(Poste.BATTEUR);
                c.GenererJoueur(Poste.BATTEUR);
                c.GenererJoueur(Poste.POURSUIVEUR);
                c.GenererJoueur(Poste.POURSUIVEUR);
                c.GenererJoueur(Poste.POURSUIVEUR);
            }
        }
    }
}