using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointA : MonoBehaviour
{
    public GameObject finishline;
    void OnCollisionEnter(Collision collision){
        if (collision.gameObject.name == "kart")
        {
            if (finishline.GetComponent<finishline>().checkpointB == 0 && finishline.GetComponent<finishline>().checkpointC==0)
            {
                finishline.GetComponent<finishline>().checkpointA = 1;
            }
            
        }

    }

}
