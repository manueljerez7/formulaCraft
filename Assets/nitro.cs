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
            soundManager.PlaySound(soundManager.Sound.PowerUpSound);
            kart.GetComponent<kartscript>().nitrocap += 1.0f;
            Destroy(gameObject);
        }
    }
}