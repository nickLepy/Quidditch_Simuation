using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class Deplacement
{
    public static void Freinage(Rigidbody rb)
    {
        rb.velocity *= 0.9f;
        rb.angularVelocity *= 0.5f;
    }

    public static bool CollisionBordure(Transform transform)
    {
        bool res = false;

        if (transform.position.y < Constantes3D.BORDURE_BASSE) res = true; //+1 pour éviter qu'il s'enfonce dans le sol
        if (transform.position.y > Constantes3D.BORDURE_HAUTE) res = true;
        if (transform.position.z < Constantes3D.BORDURE_DERRIERE) res = true;
        if (transform.position.z > Constantes3D.BORDURE_DEVANT) res = true;
        if (transform.position.x < Constantes3D.BORDURE_GAUCHE) res = true;
        if (transform.position.x > Constantes3D.BORDURE_DROITE) res = true;

        return res;
    }

    public static void CheckBordures(Transform transform)
    {
        if (transform.position.y < Constantes3D.BORDURE_BASSE) transform.position = new Vector3(transform.position.x, Constantes3D.BORDURE_BASSE, transform.position.z);
        if (transform.position.y > Constantes3D.BORDURE_HAUTE) transform.position = new Vector3(transform.position.x, Constantes3D.BORDURE_HAUTE, transform.position.z);
        if (transform.position.z < Constantes3D.BORDURE_DERRIERE) transform.position = new Vector3(transform.position.x, transform.position.y, Constantes3D.BORDURE_DERRIERE);
        if (transform.position.z > Constantes3D.BORDURE_DEVANT) transform.position = new Vector3(transform.position.x, transform.position.y, Constantes3D.BORDURE_DEVANT);
        if (transform.position.x < Constantes3D.BORDURE_GAUCHE) transform.position = new Vector3(Constantes3D.BORDURE_GAUCHE, transform.position.y, transform.position.z);
        if (transform.position.x > Constantes3D.BORDURE_DROITE) transform.position = new Vector3(Constantes3D.BORDURE_DROITE, transform.position.y, transform.position.z);
    }

    public static Vector3 Direction(Vector3 objet, Vector3 destination, int vitesse)
    {
        return (destination - objet).normalized * vitesse;
    }

    /// <summary>
    /// Cages de l'équipe adverse, là où l'équipe doit marquer
    /// </summary>
    /// <param name="match"></param>
    /// <param name="club"></param>
    /// <returns></returns>
    public static GameObject Cages(Match match, Club club)
    {
        GameObject res = null;

        if (club == match.Equipe1)
            res = GameObject.Find("CageDevant");
        else if (club == match.Equipe2)
            res = GameObject.Find("CageDerriere");

        return res;
    }

    /// <summary>
    /// Cage que défend l'équipe
    /// </summary>
    /// <param name="match"></param>
    /// <param name="club"></param>
    /// <returns></returns>
    public static GameObject CagesEquipe(Match match, Club club)
    {
        GameObject res = null;

        if (club == match.Equipe1)
            res = GameObject.Find("CageDerriere");
        else
            res = GameObject.Find("CageDevant");

        return res;
    }

    public static GameObject Cage(Match match, Club club)
    {
        GameObject res = null;
        GameObject cages = Cages(match, club);
        if(cages != null)
        {
            int random = UnityEngine.Random.Range(0, 2);
            res = cages.transform.GetChild(random).Find("Cylinder").gameObject;
        }

        return res;
    }

}