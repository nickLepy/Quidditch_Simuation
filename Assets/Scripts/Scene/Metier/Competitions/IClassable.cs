using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

/// <summary>
/// Permet à un tour de posséder un classement (exemple pour les championnats ou les poules)
/// </summary>
public interface IClassable
{
    int Points(Club c);
}