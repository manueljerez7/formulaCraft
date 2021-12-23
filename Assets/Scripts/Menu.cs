using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
	public string name;
	public GameObject inputField;
    private GameObject backgroundMusicMenu;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        name = PlayerPrefs.GetString("username");
        if(SceneManager.GetActiveScene().name == "MainMenu"){
        if(name!=null)
        {
            inputField.GetComponent<Text>().text = name;
        }
        }
        backgroundMusicMenu = GameObject.Find("BackgroundMusic");
    }


    public void PlayGame(){
		name = inputField.GetComponent<Text>().text;
		if(name=="") {
			name= "Unnamed";
 		}
		PlayerPrefs.SetString("username",name);
        SceneManager.LoadScene("PlayMenu");
    }

    public void PlaySnowyCourse()
    {
        backgroundMusicMenu.SendMessage("StopMenuMusic", 0.0f);
        SceneManager.LoadScene("LootingArea");
	}

    public void PlayDesertTour()
    {
        backgroundMusicMenu.SendMessage("StopMenuMusic", 0.0f);
        SceneManager.LoadScene("LootingArea2");
	}
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void ReturnToMainMenu()
    {
    	SceneManager.LoadScene("MainMenu");
	}

	public void ControlsMenu()
	{
    	SceneManager.LoadScene("ControlsMenu");
    }

	public void OptionsMenu()
	{
    	SceneManager.LoadScene("OptionsMenu");		
	}

	public void Leaderboard()
	{
    	SceneManager.LoadScene("LeaderboardMenu");		
	}

    public void LeaderboardOne()
	{
    	SceneManager.LoadScene("Leaderboard1Menu");		
	}

    public void LeaderboardTwo()
	{
    	SceneManager.LoadScene("Leaderboard2Menu");		
	}

    public void SetMasterVolume(float volume)
    {
        GameAssets.i.audioMixer.SetFloat("masterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        GameAssets.i.audioMixer.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        GameAssets.i.audioMixer.SetFloat("sfxOtherVolume", volume);
        GameAssets.i.audioMixer.SetFloat("sfxCarVolume", volume-25.0f);
    }
}
