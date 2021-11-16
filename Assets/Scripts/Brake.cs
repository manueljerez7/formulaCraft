using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brake : CarPart
{
    // Start is called before the first frame update
    void Start()
    {
        
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

    public override void DisableCollider()
    {
        Collider boxCollider = gameObject.GetComponent<CapsuleCollider>();
        boxCollider.enabled = false;
    }

    public override void enableCollider()
    {
        Collider boxCollider = gameObject.GetComponent<CapsuleCollider>();
        boxCollider.enabled = true;
    }
}
