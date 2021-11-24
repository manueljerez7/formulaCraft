using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	public string name;
	public GameObject inputField;

    public void PlayGame(){
		name = inputField.GetComponent<Text>().text;
		if(name=="") {
      		print("Null name");
			name= "Unnamed";
 		}
		PlayerPrefs.SetString("username",name);
        SceneManager.LoadScene("LootingArea");
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
}
