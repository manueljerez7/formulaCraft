using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //for the scene change

public class Portal : MonoBehaviour
{
    [SerializeField] InteractionController controller;

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
        //change scene to racing area
        SceneManager.LoadScene("StubRacingScene"); //TODO change number 1 for whatever scene number the racing area is
    }

    public void enableAndShow()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }
}