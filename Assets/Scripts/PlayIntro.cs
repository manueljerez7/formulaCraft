using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayIntro : MonoBehaviour
{

    void Start () 
    {
        StartCoroutine("changeScene");
        PlayerPrefs.SetString("username","Write your name here");
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }
}
