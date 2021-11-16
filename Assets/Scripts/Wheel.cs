using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : CarPart
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //TODO: repeat in engine and brake
    private void SetModelByRarity()
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
}
