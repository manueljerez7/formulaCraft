using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarPart : MonoBehaviour
{
    //the rarity (aka category, value) of the car part
    //the higher the better
    //maybe we'll remove this from the mother class
    [SerializeField] public uint rarity = 0;
    
    
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
        
        //make it invisible
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        mr.enabled = false;
        
        //make it intangible
        DisableCollider();
        
        //freeze it so it doesn't fall into the void basically
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        
        //if something doesnt work, uncomment this. but pretty sure its unnecessary
        //takeToOtherLayer();
    }
    public void outFromInventory()
    {
	    //undo everything from addedToInventory()
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        mr.enabled = true;
        enableCollider();
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        
        //if something doesnt work, uncomment this. but pretty sure its unnecessary
		//returnToLayer();
    }
    
    //extracted because each type of part has a different kind of collider
    public abstract void DisableCollider();
    public abstract void enableCollider();
    
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

	public abstract void SetModelByRarity();
}