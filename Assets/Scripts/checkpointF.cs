using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointF : MonoBehaviour
{
    public GameObject finishline;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "kart")
        {
            if (finishline.GetComponent<finishline>().checkpointA == 1 && finishline.GetComponent<finishline>().checkpointB==1 && finishline.GetComponent<finishline>().checkpointC==1 && finishline.GetComponent<finishline>().checkpointD==1 && finishline.GetComponent<finishline>().checkpointE==1 )
            {
                finishline.GetComponent<finishline>().checkpointF = 1;
            }
        }
    }
}
