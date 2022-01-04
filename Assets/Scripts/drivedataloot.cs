using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class drivedataloot : MonoBehaviour
{

    public Text pausemenu;
    public CanvasGroup backgroundpause;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PauseGame ()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame ()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

	    //Pause/Resume Game
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                Cursor.visible = false;
                ResumeGame();
				backgroundpause.alpha = 0;
                pausemenu.text = "";
                Cursor.visible = false;
            }
            else
            {
                PauseGame();
                Cursor.visible = false;
				backgroundpause.alpha = 0.46f;
                pausemenu.text = "GAME PAUSED\n\nPress ESC to Resume\nPress T to go back to Menu\n"
                +"Move:WASD\nJump:Space\nRun:Left Shift\nGrab:Left Click";
                Cursor.visible = false;

				
            }
        }

		if (Input.GetKeyUp("t")){
			if(Time.timeScale == 0){
				SceneManager.LoadScene("MainMenu");
				Time.timeScale = 1;
			}
		}
    }
}