using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStats : MonoBehaviour {

    private bool _statBox = false;

	public void onClick()
    {
        _statBox = !_statBox;
        if(_statBox)
        {
            GameObject.Find("TirsE1T").GetComponent<Text>().text = Match.LeMatch.Equipe1.Nom;
            GameObject.Find("TirsE2T").GetComponent<Text>().text = Match.LeMatch.Equipe2.Nom;
            GameObject.Find("TirsE1").GetComponent<Text>().text = Match.LeMatch.Tirs1.ToString();
            GameObject.Find("TirsE2").GetComponent<Text>().text = Match.LeMatch.Tirs2.ToString();
            GameObject.Find("PE1T").GetComponent<Text>().text = Match.LeMatch.Equipe1.Nom;
            GameObject.Find("PE2T").GetComponent<Text>().text = Match.LeMatch.Equipe2.Nom;
            GameObject.Find("PE1").GetComponent<Text>().text = (Match.LeMatch.Possession1*100).ToString("0") + "%";
            GameObject.Find("PE2").GetComponent<Text>().text = (Match.LeMatch.Possession2*100).ToString("0") + "%";
            GameObject.Find("StatsBox").GetComponent<CanvasGroup>().alpha = 1f;
        }
        else
        {
            GameObject.Find("StatsBox").GetComponent<CanvasGroup>().alpha = 0f;
        }
        
    }
}
