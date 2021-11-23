using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstaclehorisontal : MonoBehaviour
{
    public float distance = 5f;
    public float offset = 0f;
    public bool horizontal = true;
    private bool isForward = true;
    public float speed = 2f;
    private Vector3 startPos;

    void Awake()
    {
        startPos = transform.position;
        if (horizontal)
        {
            transform.position += Vector3.right * offset;
        }
        else
            transform.position += Vector3.forward * offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (horizontal)
        {
            if (isForward)
            {
                if (transform.position.x < startPos.x + distance)
                {
                    transform.position += Vector3.right * Time.deltaTime * speed;
                }
                else
                    isForward = false;
            }
            else
            {
                if (transform.position.x > startPos.x)
                {
                    transform.position -= Vector3.right * Time.deltaTime * speed;
                }
                else
                    isForward = true;
            }
        }

        else
        {
            if (isForward)
            {
                if (transform.position.z < startPos.z + distance)
                {
                    transform.position += Vector3.forward * Time.deltaTime * speed;
                }
                else
                    isForward = false;
            }
            else
            {
                if (transform.position.z > startPos.z)
                {
                    transform.position -= Vector3.forward * Time.deltaTime * speed;
                }
                else
                    isForward = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}

