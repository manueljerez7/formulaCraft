using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLColor : MonoBehaviour
{
    // Start is called before the first frame update
    public Material[] materials;
    Renderer renderer;

    void Start()
    {   
        int rarity = PlayerPrefs.GetInt("FLrarity");
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
}
