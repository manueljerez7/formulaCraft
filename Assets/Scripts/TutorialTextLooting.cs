using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTextLooting : MonoBehaviour
{
    public int current_text;

    Text text;
    public string text1 = "You are now in the looting zone. Quick! Find all the car pieces to build your car and start a race.";
    public string text2 = "MOVE with WSAD or ARROWS.";
    public string text3 = "If you're in a hurry RUN with RIGHT SHIFT.";
    public string text4 = "JUMP with SPACE.";
    public string text5 = "PICK an item by LEFT CLICKING on it .";
    public string text6 = "Remember about the hierarchy GOLD pieces - the best, BLUE - middle, GREY - ok.";
    public string text7 = "Let's assume you picked all the car pieces. Now you must find a portal which leads to the racing part where real fun begins.";
    public string text8 = "See you on the other side!";

    public string[] tutorial_text;

    // Start is called before the first frame update
    void Start()
    {
        current_text = 0;
        text = gameObject.GetComponentInChildren<Text>();

        tutorial_text = new string[8];

        tutorial_text[0] = text1;
        tutorial_text[1] = text2;
        tutorial_text[2] = text3;
        tutorial_text[3] = text4;
        tutorial_text[4] = text5;
        tutorial_text[5] = text6;
        tutorial_text[6] = text7;
        tutorial_text[7] = text8;
        InvokeRepeating("ChangeText", 7, 5);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = tutorial_text[current_text];
    }

    void ChangeText()
    {
        current_text += 1;
    }
}
