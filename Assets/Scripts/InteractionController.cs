using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Camera lootingCamera;
    [SerializeField] GameObject gui;


    [SerializeField] private float _interactionDistance = 1f;

    [SerializeField] private LayerMask _interactableLayer;
    
    [SerializeField] private GameObject portal;

    public GameObject[] wheelInventory = new GameObject[4];
    public GameObject[] engineInventory = new GameObject[1];
    public GameObject[] brakeInventory = new GameObject[1];
    public bool inventoryFull = false;
    
    //public engineScript
	void Start(){
		gui = GameObject.Find("InventoryHud");
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
                    }

                    else if (objectTag == "Engine")
                    {
                        if (engineInventory[0] != null)
                        {
                            engineInventory[0].SendMessage("outFromInventory");
                        }
                        engineInventory[0] = hitInfo.collider.gameObject;
					              gui.SendMessage("addEngineGUI");
                    }


                    else if (objectTag == "Brake")
                    {
                        if (brakeInventory[0] != null)
                        {
                            brakeInventory[0].SendMessage("outFromInventory");
                        }

                        brakeInventory[0] = hitInfo.collider.gameObject;
					              gui.SendMessage("addBrakeGUI");
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
            uint worstWheelEquippedRarity = 2000; //very high number to start the loop
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
}

