using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Script associé à la caméra
/// </summary>
public class Menu : MonoBehaviour
{
    private IGUI _gui;
    private static IGUI _demarrage;
    public static IGUI Demarrage { get { return _demarrage; } set { _demarrage = value; } }

    private void Start()
    {
        //Démarrage : menu principal
        _gui = new GUI_MenuPrincipal();
        if (_demarrage != null)
            _gui = _demarrage;
    }

    private void OnGUI()
    {
        _gui = _gui.GUI();
    }
}