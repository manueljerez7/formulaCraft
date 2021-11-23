using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryHud : MonoBehaviour
{
	public int numberOfWheels = 0;
	public int numberOfEngines = 0;
	public int numberOfBrakes = 0;
	public Text display;

    // Start is called before the first frame update
    void Start()
    {
        display.text = "Wheels: "+numberOfWheels.ToString()+"/4\n"+"Engine: "+numberOfEngines.ToString()+"/1\n"+"Brake: "+numberOfBrakes.ToString()+"/1\n";
	}			

    // Update is called once per frame
    void Update()
    {
        
    }

	void addWheelGUI(){
		if(numberOfWheels<4){
		numberOfWheels++;
        display.text = "Wheels: "+numberOfWheels.ToString()+"/4\n"+"Engine: "+numberOfEngines.ToString()+"/1\n"+"Brake: "+numberOfBrakes.ToString()+"/1\n";
		}
	}

	void addEngineGUI(){
		if(numberOfEngines<1){
		numberOfEngines++;
        display.text = "Wheels: "+numberOfWheels.ToString()+"/4\n"+"Engine: "+numberOfEngines.ToString()+"/1\n"+"Brake: "+numberOfBrakes.ToString()+"/1\n";
		}
	}

	void addBrakeGUI(){
		if(numberOfBrakes<1){
		numberOfBrakes++;
        display.text = "Wheels: "+numberOfWheels.ToString()+"/4\n"+"Engine: "+numberOfEngines.ToString()+"/1\n"+"Brake: "+numberOfBrakes.ToString()+"/1\n";
		}
	}


}
