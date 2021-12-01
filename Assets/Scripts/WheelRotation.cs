using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public Transform wheel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("kart").GetComponent<kartscript>().speed>0){
        wheel.Rotate (GameObject.Find("kart").GetComponent<kartscript>().speed/20,0, 0);
        }

        if (GameObject.Find("kart").GetComponent<kartscript>().revspeed>0){
        wheel.Rotate (-GameObject.Find("kart").GetComponent<kartscript>().revspeed/20,0, 0);
        }
    }
}
