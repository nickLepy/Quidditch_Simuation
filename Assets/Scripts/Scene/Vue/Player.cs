using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Player : MonoBehaviour
{

    private bool _estFrappe;
    public bool Frappe { get { return _estFrappe; } }

    private Joueur _joueur;
    public Joueur Joueur { get { return _joueur; } set {
            _joueur = value;
            ParticleSystem.MainModule main = transform.Find("Particle System").GetComponent<ParticleSystem>().main;
            main.startColor = Utils.Couleur2Color(_joueur.Club.Couleur);
            Material mat = new Material(Shader.Find("Standard"));
            mat.color = Utils.Couleur2Color(_joueur.Club.Couleur);
            transform.Find("Player").GetComponent<MeshRenderer>().material = mat;
        } }

    private IAutomate _ia = null;
    public IAutomate IA { get { return _ia; } set { _ia = value; } }

    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody { get { return _rigidbody; } set { _rigidbody = value; } }

    private bool _convoiteSouafle;
    private bool _convoiteCognard;

    public bool ConvoiteSouafle { get { return _convoiteSouafle; } set { _convoiteSouafle = value; } }
    public bool ConvoiteCognard { get { return _convoiteCognard; } set { _convoiteCognard = value; } }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _convoiteSouafle = false;
        _convoiteCognard = false;
        _estFrappe = false;
    }

    private void FixedUpdate()
    {
        if(_ia != null && !Match.LeMatch.Termine)
        {
            _ia.Action();
            if(Souafle.Possesseur == gameObject && transform.Find("TexteNom") != null)
            {
                transform.Find("TexteNom").rotation = Quaternion.LookRotation(transform.Find("TexteNom").transform.position - Camera.main.transform.position);
            }
        }
        Deplacement.CheckBordures(transform);
        
    }

    public IEnumerator EstFrappe()
    {
        if (Souafle.Possesseur == this)
            FindObjectOfType<Souafle>().Lacher();
        _estFrappe = true;
        yield return new WaitForSeconds(3);
        _estFrappe = false;
    }
}