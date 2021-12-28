using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class drivedata : MonoBehaviour
{
    public Text speedometer;
	public Text nitroavailable;
    public Text pausemenu;
    public Rigidbody kart;
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
        speedometer.text = Mathf.Round(kart.GetComponent<kartscript>().speedometer) +" KM/h";
		nitroavailable.text = "Nitro: " + kart.GetComponent<kartscript>().nitrocap.ToString("F2") +" sec";
		
		//Pause/Resume Game
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                ResumeGame();
				backgroundpause.alpha = 0;
                pausemenu.text = "";
            }
            else
            {
                PauseGame();
				backgroundpause.alpha = 0.46f;
                pausemenu.text = "GAME PAUSED\n\nPress ESC to Resume\nPress R to Restart Racing\nPress T to go back to Menu"+
                "\nMove:WASD\nNitro:Left Shift\nBrake:Space";
				
            }
        }
		if (Input.GetKeyUp("r")){
			if(Time.timeScale == 0){
				Scene scene = SceneManager.GetActiveScene();
				SceneManager.LoadScene(scene.name);
				Time.timeScale = 1;
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