using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //for the scene change
using UnityEngine.UI;


public class PortalTutorial : MonoBehaviour
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
        soundManager.PlaySound(soundManager.Sound.TraversePortal);
        //StartCoroutine(waitAndLoadScene());
        SceneManager.LoadScene("RacingSceneTutorial");
    }
    //we wait 1.1 seconds to let the portal sound play
    /*IEnumerator waitAndLoadScene() {
        yield return new WaitForSeconds(float.Parse("0.05"));
    }*/

    public void enableAndShow()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }
}
