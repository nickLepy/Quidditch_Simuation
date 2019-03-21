using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class Session
{

    private static Session _instance = null;
    public static Session Instance
    {
        get
        {
            if (_instance == null) _instance = new Session();
            return _instance;
        }
    }

    private Partie _partie;

    public Partie Partie { get { return _partie; } set { _partie = value; } }

    private Session()
    {
        _partie = null;
    }


}