using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : CarPart
{   
    public Material[] materials;
    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.enabled=true;
        if(rarity==0){
            renderer.sharedMaterial = materials[0];
        }
        if(rarity==1){
            renderer.sharedMaterial = materials[1];
        }
        if(rarity==2){
            renderer.sharedMaterial = materials[2];
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    public override void SetModelByRarity()
    {
        //TODO: set model based on rarity
        switch(rarity) 
        {
            case 0:
                // most common rarity
                break;
            case 1:
                // mid rarity
                break;
            case 2:
                //rarest rarity
                break;
            default:
                // code block
                break;
        }
    }
    
    public override void disableCollider() {
        Collider meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.enabled = false;
    }
    
    public override void enableCollider() {
        Collider meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.enabled = true;
    }
}
