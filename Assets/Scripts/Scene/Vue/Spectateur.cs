using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Spectateur : MonoBehaviour
{

    private int _jump;
    private int _next;
    private float _y;
    private float _direction;
    private bool _sens;
    private float _vitesse;

    private void Start()
    {

        Material mat = new Material(Shader.Find("Standard"));
        mat.color = Utils.Couleur2Color(Match.LeMatch.Equipe1.Couleur);
        GetComponent<MeshRenderer>().material = mat;
        _jump = UnityEngine.Random.Range(5, 20);

        _next = 0;
        _y = transform.position.y;
        _sens = false;
        _direction = 0;
        _vitesse = UnityEngine.Random.Range(0.07f, 0.13f);
    }

    private void FixedUpdate()
    {

        _next++;
        if(_next == _jump)
        {
            _sens = !_sens;
            if(_sens)
            {
                _direction = _vitesse;
            }
            else
            {
                _direction = -_vitesse;
            }
            _next = 0;
            
        }
        transform.position = new Vector3(transform.position.x, transform.position.y + _direction, transform.position.z);
        
    }

}