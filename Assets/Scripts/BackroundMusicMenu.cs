using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class BackroundMusicMenu : MonoBehaviour
{
    private static BackroundMusicMenu backroundMusicMenuInstance;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);

        if(backroundMusicMenuInstance == null)
        {
            backroundMusicMenuInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StopMenuMusic()
    {
        SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        //AudioSource a = GetComponent<AudioSource>();
        //a.Stop();
    }
}
