using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public abstract class Etat
{
    protected IAutomate _automate;

    public Etat(IAutomate automate)
    {
        _automate = automate;
    }

    public abstract void Action();
    public abstract Etat Suivant();
}
