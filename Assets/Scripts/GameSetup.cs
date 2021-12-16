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
            soundManager.PlayBackgroundMusic(soundManager.Sound.BackgroundMusicLooting1);
        }
        
        else if (currentSceneName == "LootingArea2")
        {
            soundManager.PlayBackgroundMusic(soundManager.Sound.BackgroundMusicLootingEgipt);
        }

        else if (currentSceneName == "RacingScene")
        {
            soundManager.PlayBackgroundMusic(soundManager.Sound.BackgroundMusicRacing);
        }

        InvokeRepeating("DestroyAllSounds", 15.0f, 15.0f);
    }

    void DestroyAllSounds()
    {
        GameObject[] sounds = GameObject.FindGameObjectsWithTag("Sound");
        for (int i = 0; i < sounds.Length; i++)
        {
            if (!sounds[i].GetComponent<AudioSource>().isPlaying)
            {
                Destroy(sounds[i]);
            }
        }
    }
}
