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
            inputField.GetComponent<InputField>().text = name;
        }
        }
        backgroundMusicMenu = GameObject.Find("BackgroundMusic");
    }

    public void PlayTutorial(){
	    name = inputField.GetComponent<InputField>().text; //changed <Text> to <InputField>
		if(name=="") {
			name= "Unnamed";
 		}
		PlayerPrefs.SetString("username",name);
        backgroundMusicMenu.SendMessage("StopMenuMusic", 0.0f);
        SceneManager.LoadScene("LootingAreaTutorial");
    }

    public void PlayGame(){
		name = inputField.GetComponent<InputField>().text;
		if(name=="Write your name here") {
			name= "Unnamed";
 		}
		PlayerPrefs.SetString("username",name);
        SceneManager.LoadScene("PlayMenu");
    }

    public void PlayCredits()
    {
        SceneManager.LoadScene("CreditsMenu");
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

    public void PlaySynthwaveRoad()
    {
        backgroundMusicMenu.SendMessage("StopMenuMusic", 0.0f);
        SceneManager.LoadScene("LootingArea3");
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

    public void LeaderboardThree()
	{
    	SceneManager.LoadScene("Leaderboard3Menu");		
	}

    public void SetMasterVolume(float volume)
    {
        GameAssets.i.audioMixer.SetFloat("masterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        GameAssets.i.audioMixer.SetFloat("musicVolume", volume);
        //sfx car mixer is a child of music mixer
        GameAssets.i.audioMixer.SetFloat("sfxCarVolume", 20.0f);
    }

    public void SetSFXVolume(float volume)
    {
        GameAssets.i.audioMixer.SetFloat("sfxOtherVolume", volume);
    }
}
