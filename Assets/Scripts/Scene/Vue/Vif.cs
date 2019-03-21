using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Vif : MonoBehaviour
{
    private int _vitesse;
    private Rigidbody _rigidbody;

    private Vector3 _destination;

    private static GameObject _vif = null;
    public static GameObject Objet
    {
        get
        {
            if (_vif == null)
                _vif = GameObject.Find("Vif");
            return _vif;
        }
    }

    private void Start()
    {
        _vitesse = 2000;
        _rigidbody = GetComponent<Rigidbody>();
        _destination = PositionAleatoire();
        _rigidbody.AddForce(-(transform.position - _destination).normalized * _vitesse);

    }

    private void OnTriggerEnter(Collider other)
    {
        Player p = null;
        try
        {
            p = other.transform.parent.GetComponent<Player>();
        }
        catch
        {
            
        }
        if(p!=null)
        {
            if(p.Joueur.Poste == Poste.ATTRAPEUR && UnityEngine.Random.Range(1,4) == 1)
            {
                Camera.main.gameObject.GetComponent<Main>().Commentaire = "Ohh " + p.Joueur.Nom + " attrape le vif d'or pour " + p.Joueur.Club.Nom;
                Debug.Log("Vif attrapé");
                Match match = Match.LeMatch;
                if(p.Joueur.Club == match.Equipe1)
                {
                    match.Score1 += 150;
                    match.Vif = true;
                }
                else
                {
                    match.Score2 += 150;
                    match.Vif = false;
                }
                match.Termine = true;
                Camera.main.gameObject.GetComponent<Main>().LancerCoroutineFin();
            }
            else if(p.Joueur.Poste == Poste.ATTRAPEUR)
            {
                Camera.main.gameObject.GetComponent<Main>().Commentaire = "Attention " + p.Joueur.Nom + " a bien failli prendre le vif d'or !";
            }
        }
    }

    private Vector3 PositionAleatoire()
    {   
        int x = UnityEngine.Random.Range(Constantes3D.BORDURE_GAUCHE, Constantes3D.BORDURE_DROITE);
        int y = UnityEngine.Random.Range(Constantes3D.BORDURE_BASSE, Constantes3D.BORDURE_HAUTE);
        int z = UnityEngine.Random.Range(Constantes3D.BORDURE_DERRIERE, Constantes3D.BORDURE_DEVANT);
        return new Vector3(x, y, z);
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(_destination,transform.position) < 5)
        {
            _rigidbody.velocity = Vector3.zero;
            _destination = PositionAleatoire();
            _rigidbody.AddForce(-(transform.position-_destination).normalized * _vitesse);
        }

    }
}
