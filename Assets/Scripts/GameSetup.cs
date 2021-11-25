using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//game setup
public class GameSetup : MonoBehaviour
{
    private string currentSceneName;
    private void Awake()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        print(currentSceneName);
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
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
