using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextRacing : MonoBehaviour
{
    Text text;
    //basic movement - part 1
    public string text1;
    public string text2;
    public string text3;
    public string text4;
    //nitro - part 2
    public string text5 = "Around the track you’ll find Nitro Containers that you can pick up. Hit them and they’ll fill your Nitro meter.";
    public string text6;
    //menus - part 3
    public string text7;
    public string text8;
    //flip and end - part 4
    public string text9;
    public string text10;

    public string[] tutorial_text;

    //public GameObject spawing_manager;
    //[SerializeField] InteractionController controller;

    private bool isBoxActive;
    private int current_text;
    private int tutorial_part;

    // Start is called before the first frame update
    void Start()
    {
        tutorial_part = 1;
        current_text = 0;
        isBoxActive = false;
        text = gameObject.GetComponentInChildren<Text>();

        SetTutorialPart(tutorial_part);
        InvokeRepeating("ChangeVisibliityBox", 3, 5);
    }

    void SetTutorialPart(int part)
    {
        if (part == 1)
        {
            tutorial_text = new string[4];
            tutorial_text[0] = text1;
            tutorial_text[1] = text2;
            tutorial_text[2] = text3;
            tutorial_text[3] = text4;
            //TODO gotta fix all this
        }
        if (part == 2)
        {
            tutorial_text = new string[2];
            tutorial_text[0] = text5;
            tutorial_text[1] = text6;
        }
        if (part == 3)
        {
            tutorial_text = new string[2];
            tutorial_text[0] = text7;
            tutorial_text[1] = text8;
        }

        if (part == 4)
        {
            tutorial_text = new string[2];
            tutorial_text[0] = text9;
            tutorial_text[1] = text10;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //TODO wasteful to do in update()?
        print("current_text = "+current_text);
        text.text = tutorial_text[current_text];
    }

    void ChangeVisibliityBox()
    {
        gameObject.active = isBoxActive;
        isBoxActive = !isBoxActive;
        if(!gameObject.active)
        {
            current_text += 1;
            if (tutorial_part == 1 && current_text > 3)
            {
                ChangeTutorialPart();
            }
            if (tutorial_part == 2 && current_text > 1)
            {
                //maybe remove
                ChangeTutorialPart();
                
                //TODO wtf
                /*CancelInvoke();
                InvokeRepeating("WaitForThePortal", 0.0f, 1.0f);*/
            }
            if (tutorial_part == 3 && current_text > 1)
            {
                //maybe remove
                ChangeTutorialPart();
                
                //TODO wtf
                //CancelInvoke();
            }
        }

        if (tutorial_part == 2 && current_text == 1)
        {
            //TODO: spawn nitro
        }

        if (tutorial_part == 4 && current_text == 1)
        {
            //TODO: flip car
        }
    }

    void ChangeTutorialPart()
    {
        current_text = 0;
        tutorial_part += 1;
        SetTutorialPart(tutorial_part);
    }

    //Waits till inventory full to progress
    /*private void WaitForThePortal()
    {
        //if youve picked up everything
        if(controller.inventoryFull)
        {
            CancelInvoke();
            ChangeTutorialPart();
            InvokeRepeating("ChangeVisibliityBox", 0, 5);
        }
    }*/
}
