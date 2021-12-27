using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nitro : MonoBehaviour
{
    public Rigidbody kart;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            kart.GetComponent<kartscript>().nitrocap += 1.0f;
			gameObject.SetActive(false);
        }
    }

    //method used in the tutorial.
    //makes the nitro visible and enables it.
    //this is because in the tutorial starts with no nitros (they're invisible and disabled) 
    public void MakeVisibleAndEnable()
    {
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        mr.enabled = true;
        SphereCollider sc = gameObject.GetComponent<SphereCollider>();
        sc.enabled = true;
    }
}
