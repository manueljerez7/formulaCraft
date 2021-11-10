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
                if (hitInfo.collider.tag == "Wheel")
                {
                    AddWheel(hitInfo.collider.gameObject);
                }

                if (hitInfo.collider.tag == "Engine")
                {
                    engineInventory[0] = hitInfo.collider.gameObject;
                }

                if (hitInfo.collider.tag == "Brake")
                {
                    brakeInventory[0] = hitInfo.collider.gameObject;
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
                print(item.name + " added in a free spot");
                break;
            }
        }
                    
//If there are not free spots, we take out the first item
        if (added == false)
        {
            GameObject takenOut = wheelInventory[0];
            wheelInventory[0] = item;
            print(item.name + " added and " + takenOut.name + " discarded");
        }

    }
}

