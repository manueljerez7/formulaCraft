using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Camera lootingCamera;

    [SerializeField] private float _interactionDistance = 1f;

    [SerializeField] private LayerMask _interactableLayer;

    public GameObject[] wheelInventory = new GameObject[4];
    public GameObject[] engineInventory = new GameObject[1];
    public GameObject[] brakeInventory = new GameObject[1];
    
    //public engineScript


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            Ray ray = lootingCamera.ViewportPointToRay(new Vector3(x: 0.5f, y: 0.5f, z: 0f));
            bool hitSomething = Physics.Raycast(ray, out hitInfo, _interactionDistance, (int) _interactableLayer);
            if (hitSomething)
            {
                print("Hit " + hitInfo.collider.tag);
                
                // add the hit item to the inventory
                if (hitInfo.collider.tag == "Wheel")
                {
                    AddWheel(hitInfo.collider.gameObject);
                }

                if (hitInfo.collider.tag == "Engine")
                {
                    if (engineInventory[0] != null)
                    {
                        engineInventory[0].SendMessage("outFromInventory");
                    }

                    engineInventory[0] = hitInfo.collider.gameObject;
                }

                if (hitInfo.collider.tag == "Brake")
                {
                    if (brakeInventory[0] != null)
                    {
                        brakeInventory[0].SendMessage("outFromInventory");
                    }
                    brakeInventory[0] = hitInfo.collider.gameObject;
                }
                
                //make the item disappear from the 3D space
                hitInfo.collider.SendMessage("addedToInventory");
            }
        }
    }

    public void AddWheel(GameObject item)
    {
        item.SendMessage("addedToInventory");
        
        bool added = false;
        //We try to find a free spot on the inventory
        for (int i = 0; i < wheelInventory.Length; i++)
        { 
            if (wheelInventory[i] == null)
            {
                wheelInventory[i] = item;
                added = true;
                print(item.name + " added in a free spot");
                break;
            }
        }
                    
//If there are not free spots, we take out the first item
        if (added == false)
        {
            //to do: find the equiped wheel with the lowest rarity. we could also implement a PriorityQueue
            GameObject takenOut = wheelInventory[0];
            wheelInventory[0] = item;
            print(item.name + " added and " + takenOut.name + " discarded");
            takenOut.SendMessage("outFromInventory");
        }

    }
}

