using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public interface VueTour
{

    List<Match> Calendrier();
    void Classement();
    List<Match> ProchainsMatchs();

}