using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarPart : MonoBehaviour
{
    //the rarity (aka category, value) of the car part
    //the higher the better
    //maybe we'll remove this from the mother class
    [SerializeField] public uint rarity = 0;
    private GameObject m_Player;
    int distanceToRespawn = 3;
    
    
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
	    m_Player = GameObject.FindWithTag("Player");
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
		transform.position = m_Player.transform.position +distanceToRespawn* m_Player.transform.forward;
		
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

	public void setRarity(uint newRarity)
	{
		rarity = newRarity;
	}
	public abstract void SetModelByRarity();
}