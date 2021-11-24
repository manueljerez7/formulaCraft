using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("LootingArea");
        soundManager.PlaySound(soundManager.Sound.KartFlip);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
