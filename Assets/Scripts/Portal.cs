using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //for the scene change
using UnityEngine.UI;


public class Portal : MonoBehaviour
{
    [SerializeField] InteractionController controller;
    [SerializeField] Text timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //call the function that generates the values of performance of the car
        controller.SendMessage("GenerateValues");
        timer.SendMessage("StopLootingTimer");
        //change scene to racing area
        soundManager.PlaySound(soundManager.Sound.TraversePortal);
        if(SceneManager.GetActiveScene().name=="LootingArea"){
            SceneManager.LoadScene("RacingScene");
        }

        if(SceneManager.GetActiveScene().name=="LootingArea2"){
            SceneManager.LoadScene("DesertRacing"); 
        }

        if(SceneManager.GetActiveScene().name=="LootingArea3"){
            SceneManager.LoadScene("Cota"); 
        }
    }

    public void enableAndShow()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }
}
