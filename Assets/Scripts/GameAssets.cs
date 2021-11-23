using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class GameAssets : MonoBehaviour
{
    //public AudioClip kartAccelerate;
    //public AudioClip kartDecelerate;
    //public AudioClip kartBreaks;
    //public AudioClip kartHitSound;
    //public AudioClip kartNitro;
    //public AudioClip kartFinishLap;


    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null)_i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }

    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public soundManager.Sound sound;
        public AudioClip audioClip;

    }

}
