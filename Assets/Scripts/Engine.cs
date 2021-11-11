using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addedToInventory()
    {
        //it should also stop existing as a phisical object, since its physics are still being calculated.
        //it may not be a problem since we may remove all physics from it (Fortnite style)
        //setActive hasn't worked for me because it disappears completely and therefore can't be re-enabled
        
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        mr.enabled = false;
    }

    public void outFromInventory()
    {
        MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
        mr.enabled = true;
    }
}
