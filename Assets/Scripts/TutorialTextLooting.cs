using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextLooting : MonoBehaviour
{
    Text text;
    //moving arround - part 1
    public string text1 = "You are now in the looting zone. Quick! Find all the car pieces to build your car.";
    public string text11 = "But first ... a warm up!";
    public string text2 = "MOVE with WSAD or ARROWS.";
    public string text3 = "If you're in a hurry RUN with LEFT SHIFT.";
    public string text4 = "JUMP with SPACE.";
    //item picking - part 2
    public string text5 = "Moving is fun but driving is even better so let's build your car!";
    public string text55 = "PICK an item by LEFT CLICKING on it .";
    public string text6 = "Remember: GOLD pieces are the best, BLUE - cool but not outstanding and GREY ... they're simply ok.";
    //portal - part 3
    public string text7 = "Congratulations! You picked all the car pieces.";
    public string text77 = "Now you must find a portal leading to the racing part. Go quick, every second matters!";
    public string text8 = "See you on the other side!";

    public string[] tutorial_text;

    public GameObject spawing_manager;
    [SerializeField] InteractionController controller;

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
            tutorial_text = new string[5];
            tutorial_text[0] = text1;
            tutorial_text[1] = text11;
            tutorial_text[2] = text2;
            tutorial_text[3] = text3;
            tutorial_text[4] = text4;
        }
        if (part == 2)
        {
            tutorial_text = new string[3];
            tutorial_text[0] = text5;
            tutorial_text[1] = text55;
            tutorial_text[2] = text6;
        }
        if (part == 3)
        {
            tutorial_text = new string[3];
            tutorial_text[0] = text7;
            tutorial_text[1] = text77;
            tutorial_text[2] = text8;
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = tutorial_text[current_text];
    }

    void ChangeVisibliityBox()
    {
        gameObject.active = isBoxActive;
        isBoxActive = !isBoxActive;
        if(!gameObject.active)
        {
            current_text += 1;
            if (tutorial_part == 1 && current_text > 4)
            {
                ChangeTutorialPart();
            }
            if (tutorial_part == 2 && current_text > 2)
            {
                CancelInvoke();
                InvokeRepeating("WaitForThePortal", 0.0f, 1.0f);
            }
            if (tutorial_part == 3 && current_text > 2)
            {
                CancelInvoke();
            }
        }

        if (tutorial_part == 2 && current_text == 1)
        {
            spawing_manager.SendMessage("Spawn", 0, SendMessageOptions.RequireReceiver);
        }
    }

    void ChangeTutorialPart()
    {
        current_text = 0;
        tutorial_part += 1;
        SetTutorialPart(tutorial_part);
    }

    private void WaitForThePortal()
    {
        if(controller.inventoryFull)
        {
            CancelInvoke();
            ChangeTutorialPart();
            InvokeRepeating("ChangeVisibliityBox", 0, 5);
        }
    }
}
