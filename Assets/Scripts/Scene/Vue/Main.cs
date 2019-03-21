using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    private int _camera;
    private Audio _audio;
    public Audio Audio { get { return _audio; } }

    private string _commentaire;
    public string Commentaire { get { return _commentaire; } set { _commentaire = value; } }


    

    // Use this for initialization
    void Start () {
        _commentaire = "Bienvenue";
        GenererSpectateurs();
        /*RenderSettings.ambientLight = new Color(0.05f, 0.05f, 0.3f);
        RenderSettings.ambientIntensity = 0.2f;*/
        _audio = new Audio(GameObject.Find("Main Camera").gameObject);
        //_audio.AjouterSon(Son.COUPENVOI,0.4f,0.4f,false,5,false,0);
        _audio.AjouterSon(Son.STAD_BACK,0.85f,0.5f,true,0,false, 0);
        _audio.AjouterSon(Son.WIND_STRONG,0.6f,0.7f,true,0,false, 0);
        _audio.AjouterSon(Son.SML_STAD_BACKGROUND, 0.6f, 1, true,0,false, 0);
        _audio.AjouterSon(Son.MUSIQUE, 0.7f, 1, false, 15, false, 0);


        _camera = 1;
        GameObject.Find("Main Camera").transform.position = new Vector3(-83, 25, -10);
        GameObject.Find("Main Camera").transform.position = new Vector3(-70, 26, 0);
        GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(new Vector3(15, 90, 0));
        if(Match.LeMatch == null)
        {
            if(Session.Instance.Partie == null)
            {
                Session.Instance.Partie = new Partie(new Gestionnaire());
            }
            Gestionnaire gestionnaire = Session.Instance.Partie.Gestionnaire;
            ParseurXML xml = new ParseurXML(gestionnaire);
            xml.Charger();
            Match.LeMatch = new Match(gestionnaire.ObtenirClub(1), gestionnaire.ObtenirClub(2), new System.DateTime(2018,10,10,20,0,0));
        }

        Match _match = Match.LeMatch;


        /*
        foreach(Club c in Gestionnaire.Instance.Clubs)
        {
            foreach(Joueur j in c.Joueurs)
            {
                j.Vitesse /= 2;
                j.Precision /= 2;
            }
        }*/


        foreach (Joueur j in _match.Equipe1.Joueurs)
        {
            j.Vitesse = (int)(j.Vitesse * 0.5f);
            GenererJoueur(j);
        }
        foreach(Joueur j in _match.Equipe2.Joueurs)
        {
            j.Vitesse = (int)(j.Vitesse * 0.5f);
            GenererJoueur(j);
        }
        StartCoroutine(Ambiance());
    }

    void GenererJoueur(Joueur joueur)
    {
        //if(joueur.Poste != Poste.BATTEUR)
        //{
            GameObject player = Resources.Load("Prefabs/Player", typeof(GameObject)) as GameObject;
            player = Instantiate(player);
            player.transform.position = new Vector3(Random.Range(Constantes3D.BORDURE_GAUCHE, Constantes3D.BORDURE_DROITE), Random.Range(Constantes3D.BORDURE_BASSE, Constantes3D.BORDURE_HAUTE), Random.Range(Constantes3D.BORDURE_DERRIERE, Constantes3D.BORDURE_DEVANT));
            Player script = player.AddComponent<Player>();
            script.Joueur = joueur;
            switch (joueur.Poste)
            {
                case Poste.GARDIEN:
                    script.IA = new Automate_Gardien(script);
                    break;
                case Poste.POURSUIVEUR:
                    script.IA = new Automate_Poursuiveur(script);
                    break;
                case Poste.BATTEUR:
                    script.IA = new Automate_Batteur(script);
                    break;
                case Poste.ATTRAPEUR:
                    script.IA = new Automate_Attrapeur(script);
                    break;
            }
        //}

    }

    // Update is called once per frame
    void FixedUpdate () {
        Match _match = Match.LeMatch;
        _match.Temps = _match.Temps.AddSeconds(0.03f * 10);
        GameObject.Find("Commentaire").GetComponent<Text>().text = _commentaire;
        GameObject.Find("Equipe1").GetComponent<Text>().text = _match.Equipe1.Nom;
        GameObject.Find("Score1").GetComponent<Text>().text = _match.Score2.ToString();
        GameObject.Find("Score2").GetComponent<Text>().text = _match.Score1.ToString();
        GameObject.Find("Equipe2").GetComponent<Text>().text = _match.Equipe2.Nom;
        GameObject.Find("Temps").GetComponent<Text>().text = _match.Temps.ToString("HH:mm:ss");
        if (Input.GetKeyUp(KeyCode.C))
        {
            _camera++;
            _camera = _camera % 4;
            switch(_camera)
            {
                case 0:
                    GameObject.Find("Main Camera").transform.position = new Vector3(-70, 26, 0);
                    GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(new Vector3(15, 90, 0));
                    break;
                case 1:
                    GameObject.Find("Main Camera").transform.position = new Vector3(0, 25, -105);
                    GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(new Vector3(20, 0, 0));
                    break;
                case 2:
                    GameObject.Find("Main Camera").transform.position = new Vector3(0, 25, 105);
                    GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(new Vector3(20, 180, 0));
                    break;
                case 3:
                    GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(new Vector3(15, 90, 0));
                    break;
            }
        }
        if(_camera == 3)
        {
            //Position x : on recule de 30 mètres, ainsi que de la vitesse en m/s du souafle / 10, pour voir un peu plus large quand le souafle bouge
            Camera.main.transform.position = new Vector3(Souafle.Objet.transform.position.x-33 /*- (Souafle.Objet.GetComponent<Rigidbody>().velocity.magnitude / 10)*/, Souafle.Objet.transform.position.y+10, Souafle.Objet.transform.position.z);
        }
        
    }

    IEnumerator Ambiance()
    {
        while (!Match.LeMatch.Termine)
        {
            yield return new WaitForSeconds(5);
            if(UnityEngine.Random.Range(1,4) == 1)
            {
                _audio.AjouterSon(Son.TAMBOURS2, 0.6f, 0.8f, false, 0, true, 0);
            }

            if (UnityEngine.Random.Range(1, 4) == 1)
            {
                _audio.AjouterSon(Son.EXPLOSION, 0.8f, 1, false,0, true, 0);
                Fumigene();
            }
        }
    }

    private void Fumigene()
    {
        Spectateur s = FindObjectsOfType<Spectateur>()[Random.Range(0, FindObjectsOfType<Spectateur>().Length)];
        GameObject fumi = Resources.Load("Prefabs/Fumigene", typeof(GameObject)) as GameObject;
        fumi = Instantiate(fumi, s.transform.position, Quaternion.Euler(-76.15f, 256, -258));
        Commentaire = "Il y a de l'ambiance dans les tribunes.";
    }

    IEnumerator TempoSon(float temps, AudioSource source)
    {
        yield return new WaitForSeconds(temps);
        source.Stop();
        Destroy(source);
    }

    public void LancerCoroutine(float temps, AudioSource source)
    {
        StartCoroutine(TempoSon(temps, source));
    }

    public void LancerCoroutineFin()
    {
        StartCoroutine(Fin());
    }

    IEnumerator Fin()
    {
        _audio.AjouterSon(Son.LRG_STAD_GOAL, 0.8f, 1f, false, 0, false, 0);
        yield return new WaitForSeconds(5);
        if(Session.Instance.Partie.Club != null)
            Menu.Demarrage = new GUI_AcceuilCompetition();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void GenererSpectateurs()
    {
        int nbS = 0;
        for(float i = -84; i<63; i+= 2f) //De gauche à droite
        {
            float h = 1.4f;
            for(float j = -64f; j>-84; j -= 2f) //Espace gradin
            {
                GameObject supporter = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                supporter.transform.position = new Vector3(j, h, i);
                supporter.AddComponent<Spectateur>();
                h += 0.8f;
                nbS++;
            }
        }

        for (float i = 10; i < 71; i += 2f) //De gauche à droite
        {
            float h = 1.4f;
            for (float j = 83f; j < 110; j += 2f) //Espace gradin
            {
                GameObject supporter = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                supporter.transform.position = new Vector3(j, h, i);
                supporter.AddComponent<Spectateur>();
                h += 0.8f;
                nbS++;
            }
        }

        for (float i = -80; i < -6; i += 2f) //De gauche à droite
        {
            float h = 1.4f;
            for (float j = 83f; j < 110; j += 2f) //Espace gradin
            {
                GameObject supporter = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                supporter.transform.position = new Vector3(j, h, i);
                supporter.AddComponent<Spectateur>();
                h += 0.8f;
                nbS++;
            }
        }
        Debug.Log(nbS + " spectateurs ");
    }
}