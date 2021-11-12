using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followkart : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform targetObject;
    private Vector3 initalOffset;
    private Vector3 cameraPosition;
    public float smoothness;


    void Start()
    {
        //initalOffset = transform.position - targetObject.position;
    }
    // Update is called once per frame
    void Update()
    {
        //cameraPosition = targetObject.position + initalOffset;
        //transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothness*Time.fixedDeltaTime);
    }
    
}

