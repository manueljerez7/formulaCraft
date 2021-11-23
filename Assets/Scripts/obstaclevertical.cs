using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstaclevertical : MonoBehaviour
{
    private bool isUp = true;
    public float speed = 2f;
    private Vector3 startPos;
    private float height;

    private ParticleSystem particle;

    void Awake()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        // startPos = transform.position;
        height = transform.localScale.y;
        if (isUp)
        {
            startPos.y = transform.position.y - height;
        }
        else
            startPos.y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isUp)
        {
            if (transform.position.y < startPos.y + height)
            {
                transform.position += Vector3.up * Time.deltaTime * speed;
            }
            else
                isUp = false;
        }
        else
        {
            if (transform.position.y > startPos.y)
            {
                transform.position += Vector3.down * Time.deltaTime * speed;
            }
            else
                isUp = true;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            // explosion
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        particle.Play();
        yield return new WaitForSeconds(particle.main.startLifetime.constantMax);
        Destroy(gameObject);
    }
}

