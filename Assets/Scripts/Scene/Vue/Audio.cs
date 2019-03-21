using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public enum Son
{
    BALAIS,
    BUT,
    CHANT,
    COUPENVOI,
    ENTRAINEUR,
    LRG_STAD_SIFFLET,
    MAGIC,
    LRG_STAD_GOAL,
    LRG_STAD_HAPPY,
    LRG_STAD_SIFFLET2,
    SCOREFLASH,
    SIFFLET,
    SML_STAD_HAPPY,
    SML_STAD_HAPPY2,
    SML_STAD_BACKGROUND,
    STAD_BACK,
    TAMBOURS,
    TAMBOURS2,
    WIND_STRONG,
    WIND_WEAK,
    SPEAKER,
    MUSIQUE,
    EXPLOSION,
    MENU,
    MENU2,
    MENU3
}

public class Audio
{

    private GameObject _parent;

    public Audio(GameObject parent)
    {
        _parent = parent;
    }

    public bool ActuellementJoue(string nom)
    {
        bool res = false;
        foreach(AudioSource a in Camera.main.gameObject.GetComponents<AudioSource>())
        {
            if (a.clip.name == nom) res = true;
        }
        return res;
    }

    public void AjouterSon(Son son, float volume, float spacial, bool loop, float temps, bool destroy, float delay)
    {
        if(!(son == Son.BUT && ActuellementJoue("but")))
        {
            AudioClip audio = Resources.Load("Sound/" + Son2Chemin(son), typeof(AudioClip)) as AudioClip;
            AudioSource source = _parent.AddComponent<AudioSource>();
            source.clip = audio;
            source.volume = volume;
            source.spatialBlend = spacial;
            source.loop = loop;
            if (delay != 0)
            {
                source.PlayDelayed(delay);
            }
            else
                source.Play();

            if (temps != 0)
            {
                Camera.main.gameObject.GetComponent<Main>().LancerCoroutine(temps, source);
            }
            if (destroy)
            {
                Camera.main.gameObject.GetComponent<Main>().LancerCoroutine(audio.length, source);
            }
        }
        
    }

    

    private string Son2Chemin(Son son)
    {
        string res = "";
        switch (son)
        {
            case Son.BALAIS:
                res = "balais";
                break;
            case Son.BUT:
                res = "but";
                break;
            case Son.CHANT:
                res = "chant";
                break;
            case Son.COUPENVOI:
                res = "coupenvoi";
                break;
            case Son.ENTRAINEUR:
                res = "entraineur";
                break;
            case Son.LRG_STAD_SIFFLET:
                res = "large_stadium_sifflets";
                break;
            case Son.MAGIC:
                res = "magic";
                break;
            case Son.LRG_STAD_GOAL:
                res = "medium_stadium_goal";
                break;
            case Son.LRG_STAD_HAPPY:
                res = "medium_stadium_happy";
                break;
            case Son.LRG_STAD_SIFFLET2:
                res = "medium_stadium_sifflets";
                break;
            case Son.SCOREFLASH:
                res = "scoreflash";
                break;
            case Son.SIFFLET:
                res = "sifflet";
                break;
            case Son.SML_STAD_HAPPY:
                res = "small_stadium_applause";
                break;
            case Son.SML_STAD_HAPPY2:
                res = "small_stadium_applause2";
                break;
            case Son.SML_STAD_BACKGROUND:
                res = "small_stadium_background";
                break;
            case Son.STAD_BACK:
                res = "stadiumbackground";
                break;
            case Son.TAMBOURS:
                res = "tambours";
                break;
            case Son.TAMBOURS2:
                res = "tambours2";
                break;
            case Son.WIND_STRONG:
                res = "wind_strong";
                break;
            case Son.WIND_WEAK:
                res = "wind_weak";
                break;
            case Son.SPEAKER:
                res = "speaker";
                break;
            case Son.MUSIQUE:
                res = "musique";
                break;
            case Son.EXPLOSION:
                res = "explosion";
                break;
            case Son.MENU:
                res = "menu";
                break;
            case Son.MENU2:
                res = "menu2";
                break;
            case Son.MENU3:
                res = "menu3";
                break;
        }

        return res;
    }
}