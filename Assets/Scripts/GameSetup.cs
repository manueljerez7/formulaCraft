using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

//game setup
public class GameSetup : MonoBehaviour
{
    private string currentSceneName;
    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        soundManager.Initialize();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        if (currentSceneName == "LootingArea")
        {
            soundManager.PlayBackgroundMusic(soundManager.Sound.BackgroundMusicLooting);
        }

        else if (currentSceneName == "RacingScene")
        {
            soundManager.PlayBackgroundMusic(soundManager.Sound.BackgroundMusicRacing);
        }

        InvokeRepeating("DestroyAllSounds", 15.0f, 15.0f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void DestroyAllSounds()
    {
        GameObject[] sounds = GameObject.FindGameObjectsWithTag("Sound");
        //AudioSource[] sounds = Object.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        for (int i = 0; i < sounds.Length; i++)
        {
            if (!sounds[i].GetComponent<AudioSource>().isPlaying)
            {
                Destroy(sounds[i]);
            }
        }
    }
}
