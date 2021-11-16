using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPart : MonoBehaviour
{
    //the rarity (aka category, value) of the car part
    //the higher the better
    //maybe we'll remove this from the mother class
    [SerializeField] public uint rarity=0;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addedToInventory()
    {
        //it should stop being able to be clicked.
        //it should also stop existing as a physical object, since its physics are still being calculated.
        //it may not be a problem since we may remove all physics from it (Fortnite style)
        //setActive hasn't worked for me because it disappears completely and therefore can't be re-enabled
        
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        mr.enabled = false;
        //Collider sphereCollider = gameObject.GetComponent<SphereCollider>();
        //sphereCollider.enabled = false;
		takeToOtherLayer();
    }

    public void outFromInventory()
    {
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        mr.enabled = true;
        //Collider sphereCollider = gameObject.GetComponent<SphereCollider>();
        //sphereCollider.enabled = true;
		returnToLayer();
    }

	public void takeToOtherLayer()
	{
		gameObject.layer = LayerMask.NameToLayer("InteractableDisabled");
	}

	public void returnToLayer()
	{
		gameObject.layer = LayerMask.NameToLayer("Interactable");
	}

	public void setRarity(uint newRarity)
	{
		rarity = newRarity;
	}
}