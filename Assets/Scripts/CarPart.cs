using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarPart : MonoBehaviour
{
    //the rarity (aka category, value) of the car part
    //the higher the better
    //maybe we'll remove this from the mother class
    [SerializeField] public int rarity = 0;
    private GameObject m_Player;
    int distanceToRespawn = 1;
    new Vector3 initPosition;
    
    
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
	    m_Player = GameObject.FindWithTag("Hand");
        initPosition = transform.position;
        
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
        disableCollider();
        
        //freeze it so it doesn't fall into the void basically
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        
        //if something doesnt work, uncomment this. but pretty sure its unnecessary
        //takeToOtherLayer();
    }
    public void outFromInventory()
    {
		//move it to in front of the player
        //NEEDS TO BE CHECKED
		transform.position =  initPosition; //m_Player.transform.position + new Vector3(0,2,0); //+distanceToRespawn* m_Player.transform.forward + new Vector3(0,5,0);
		
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
    public abstract void disableCollider();
    public abstract void enableCollider();
    
    public void takeToOtherLayer()
	{
		gameObject.layer = LayerMask.NameToLayer("InteractableDisabled");
	}

	public void returnToLayer()
	{
		gameObject.layer = LayerMask.NameToLayer("Interactable");
	}

	public void setRarity(int newRarity)
	{
		rarity = newRarity;
	}
	public abstract void SetModelByRarity();
}