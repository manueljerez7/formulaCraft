using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Camera lootingCamera;
    [SerializeField] GameObject gui;


    [SerializeField] private float _interactionDistance = 1.5f;

    [SerializeField] private LayerMask _interactableLayer;
    
    [SerializeField] private GameObject portal;

    public GameObject[] wheelInventory = new GameObject[4];
    public GameObject[] engineInventory = new GameObject[1];
    public GameObject[] brakeInventory = new GameObject[1];
    public bool inventoryFull = false;

    public Text carProperties;
    public float gripGUI = 0f;
    public float acc = 0f;
    public float decc = 0f;
    public float topSpeedGUI = 0f;
    
    //public engineScript
    void Start(){
		gui = GameObject.Find("InventoryHud");
        carProperties.text = "Grip: "+gripGUI.ToString()+"\n"+
            "Acceleration: "+acc.ToString()+"\n"+
            "Decceleration: "+decc.ToString()+"\n"+
            "Top Speed: "+topSpeedGUI.ToString();
}

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            Ray ray = lootingCamera.ViewportPointToRay(new Vector3(x: 0.5f, y: 0.5f, z: 0f));
            bool hitSomething = Physics.Raycast(ray, out hitInfo, _interactionDistance, (int) _interactableLayer);
            if (hitSomething)
            {
            
                string objectTag = hitInfo.collider.tag;
                print("Hit " + objectTag);

                if (objectTag == "Wheel" || objectTag == "Engine" || objectTag == "Brake") //if we hit a car part
                {

                    // add the hit item to the inventory
                    if (objectTag == "Wheel")
                    {
                        AddWheel(hitInfo.collider.gameObject);
					    gui.SendMessage("addWheelGUI");
                        print("Hola");

                        if(wheelInventory[1]==null && wheelInventory[2]==null && wheelInventory[3]==null){
                            gripGUI = wheelInventory[0].GetComponent<Wheel>().rarity;
                        }
                        if(wheelInventory[1]!=null && wheelInventory[2]==null && wheelInventory[3]==null){
                            gripGUI = (wheelInventory[0].GetComponent<Wheel>().rarity + wheelInventory[1].GetComponent<Wheel>().rarity);
                        }
                        if(wheelInventory[1]!=null && wheelInventory[2]!=null && wheelInventory[3]==null){
                            gripGUI = (wheelInventory[0].GetComponent<Wheel>().rarity + wheelInventory[1].GetComponent<Wheel>().rarity + wheelInventory[2].GetComponent<Wheel>().rarity);
                        }
                        if(wheelInventory[1]!=null && wheelInventory[2]!=null && wheelInventory[3]!=null){
                            gripGUI = (wheelInventory[0].GetComponent<Wheel>().rarity + wheelInventory[1].GetComponent<Wheel>().rarity + wheelInventory[2].GetComponent<Wheel>().rarity + wheelInventory[3].GetComponent<Wheel>().rarity);
                        }

                        carProperties.text = "Grip: "+gripGUI.ToString()+"\n"+
                        "Acceleration: "+acc.ToString()+"\n"+
                        "Decceleration: "+decc.ToString()+"\n"+
                        "Top Speed: "+topSpeedGUI.ToString();

                    }

                    else if (objectTag == "Engine")
                    {
                        if (engineInventory[0] != null)
                        {
                            engineInventory[0].SendMessage("outFromInventory");
                        }
                        engineInventory[0] = hitInfo.collider.gameObject;
					              gui.SendMessage("addEngineGUI");

                        topSpeedGUI = 150.0f + (engineInventory[0].GetComponent<Engine>().rarity)*30.0f;
                        acc = (engineInventory[0].GetComponent<Engine>().rarity)*1.5f;

                        carProperties.text = "Grip: "+gripGUI.ToString()+"\n"+
                        "Acceleration: "+acc.ToString()+"\n"+
                        "Decceleration: "+decc.ToString()+"\n"+
                        "Top Speed: "+topSpeedGUI.ToString();
                        
                    }


                    else if (objectTag == "Brake")
                    {
                        if (brakeInventory[0] != null)
                        {
                            brakeInventory[0].SendMessage("outFromInventory");
                        }

                        brakeInventory[0] = hitInfo.collider.gameObject;
					              gui.SendMessage("addBrakeGUI");

                        decc = (brakeInventory[0].GetComponent<Brake>().rarity)*0.5f;
                        carProperties.text = "Grip: "+gripGUI.ToString()+"\n"+
                        "Acceleration: "+acc.ToString()+"\n"+
                        "Decceleration: "+decc.ToString()+"\n"+
                        "Top Speed: "+topSpeedGUI.ToString();
                    }

                    //make the grabbed item disappear from the 3D space
                    hitInfo.collider.SendMessage("addedToInventory");

                    //spawn the portal if the player has all the necessary pieces 
                    if (!inventoryFull && wheelInventory[3] != null && engineInventory[0] != null &&
                        brakeInventory[0] != null)
                    {
                        inventoryFull = true;
                        showPortal();
                    }
                    soundManager.PlaySound(soundManager.Sound.GrabCarPart);   
                }
            }
        }
    }

    public void AddWheel(GameObject item)
    {
        
        bool added = false;
        //We try to find a free spot on the inventory
        for (int i = 0; i < wheelInventory.Length; i++)
        { 
            if (wheelInventory[i] == null)
            {
                wheelInventory[i] = item;
                added = true;
                item.SendMessage("addedToInventory");
                print(item.name + " added in a free spot");
                break;
            }
        }
                    
//If there are not free spots, we find an item to take out
        if (added == false)
        {
            //find the equiped wheel with the lowest rarity. (we could also implement a PriorityQueue)
            int wheelToRemove = -1;
            int worstWheelEquippedRarity = 2000; //very high number to start the loop
            for (int i = 0; i < wheelInventory.Length; i++)
            {
                print("wheel's rarity == " + wheelInventory[i].GetComponent<Wheel>().rarity + "\n worstWheelEquippedRarity == " + worstWheelEquippedRarity);
                if (wheelInventory[i].GetComponent<Wheel>().rarity < worstWheelEquippedRarity)
                {
                    worstWheelEquippedRarity = wheelInventory[i].GetComponent<Wheel>().rarity;
                    wheelToRemove = i;
                }
            }
			GameObject takenOut;
            //if we didn't find a wheel to replace we simply replace the oldest one
            if (wheelToRemove != -1){
			takenOut = wheelInventory[wheelToRemove];
			}
            else
			{
                wheelToRemove = 3;
            	takenOut = wheelInventory[0];
				wheelInventory[0]=wheelInventory[1];
				wheelInventory[1]=wheelInventory[2];
				wheelInventory[2]=wheelInventory[3];
			}
            wheelInventory[wheelToRemove] = item;
            item.SendMessage("addedToInventory");

            print(item.name + " added and " + takenOut.name + " discarded");
            
            takenOut.SendMessage("outFromInventory");
        }

    }

    private void showPortal()
    {
        portal.SendMessage("enableAndShow");
    }

	public void GenerateValues()
	{
        PlayerPrefs.SetInt("engineRarity",engineInventory[0].GetComponent<Engine>().rarity);
        PlayerPrefs.SetInt("FLrarity",wheelInventory[0].GetComponent<Wheel>().rarity);
        PlayerPrefs.SetInt("FRrarity",wheelInventory[1].GetComponent<Wheel>().rarity);
        PlayerPrefs.SetInt("RLrarity",wheelInventory[2].GetComponent<Wheel>().rarity);
        PlayerPrefs.SetInt("RRrarity",wheelInventory[3].GetComponent<Wheel>().rarity);

		float avg_wheel_rarity = (wheelInventory[0].GetComponent<Wheel>().rarity+
									wheelInventory[1].GetComponent<Wheel>().rarity+
									wheelInventory[2].GetComponent<Wheel>().rarity+
									wheelInventory[3].GetComponent<Wheel>().rarity)/4;
		float grip = 50.0f+((avg_wheel_rarity)/2.0f)*60.0f;
		float topSpeed = 150.0f + (engineInventory[0].GetComponent<Engine>().rarity)*30.0f;
		float timeToMaxSpeed = 9 - (engineInventory[0].GetComponent<Engine>().rarity)*1.5f;
		float timeToZero = 6 - (brakeInventory[0].GetComponent<Brake>().rarity)*0.5f;
		float timeToStationary = 3 - (brakeInventory[0].GetComponent<Brake>().rarity)*0.5f;
    	PlayerPrefs.SetFloat("grip", grip);
    	PlayerPrefs.SetFloat("topSpeed", topSpeed);
		PlayerPrefs.SetFloat("timeToMaxSpeed",timeToMaxSpeed);
		PlayerPrefs.SetFloat("timeToZero",timeToZero);
		PlayerPrefs.SetFloat("timeToStationary",timeToStationary);
		print("Parameters generated");
		
	}
}

