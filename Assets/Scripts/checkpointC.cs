using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointC : MonoBehaviour
{
    public GameObject finishline;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "kart")
        {
            if (finishline.GetComponent<finishline>().checkpointA == 1 && finishline.GetComponent<finishline>().checkpointB==1)
            {
                finishline.GetComponent<finishline>().checkpointC = 1;
            }
        }
    }
}
