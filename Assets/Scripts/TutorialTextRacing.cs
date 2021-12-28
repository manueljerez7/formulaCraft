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
    public string text11;

    public string[] tutorial_text;

    [SerializeField] public nitro[] Nitros;

    private bool isBoxActive;
    private int current_text;

    private bool carFlipped = false;

    // Start is called before the first frame update
    void Start()
    {
        current_text = 0;
        isBoxActive = false;
        text = gameObject.GetComponentInChildren<Text>();

        InitTextArray();
        InvokeRepeating("ChangeVisibliityBox", 3, 5);
    }

    void InitTextArray()
    {
        tutorial_text = new string[11];
            tutorial_text[0] = text1;
            tutorial_text[1] = text2;
            tutorial_text[2] = text3;
            tutorial_text[3] = text4;
            tutorial_text[4] = text5;
            tutorial_text[5] = text6;
            tutorial_text[6] = text7;
            tutorial_text[7] = text8;
            tutorial_text[8] = text9;
            tutorial_text[9] = text10;
            tutorial_text[10] = text11;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ChangeVisibliityBox()
    {
        gameObject.active = isBoxActive;
        isBoxActive = !isBoxActive;
        if(!gameObject.active)
        {
            if (current_text < tutorial_text.Length-1)
            {
                current_text += 1;
                //moved
                print("current_text = " + current_text);
                text.text = tutorial_text[current_text];
            }
            else
                CancelInvoke();
        }

        else //if it's active. these will be executed as soon as their box shows up
        {
            if (current_text == 4) //el 5
            {
                SpawnNitros();
            }

            if (current_text == 8 && !carFlipped) //el 9
            {
                MakeCarFlip();
                carFlipped = true;
            }
        }
    }

    private void SpawnNitros()
    {
        foreach (var nitro in Nitros)
        {
            nitro.SendMessage("MakeVisibleAndEnable");
        }
    }

    private void MakeCarFlip()
    {
    }
}
