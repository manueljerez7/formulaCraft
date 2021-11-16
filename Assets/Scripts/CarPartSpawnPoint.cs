using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPartSpawnPoint : MonoBehaviour
{
    [SerializeField] uint rarity;

    public GameObject[] partPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        //instantiates a carPart of random type
        GameObject carPart = Instantiate(partPrefabs[Random.Range(0, 3)], transform.position, Quaternion.identity);
        carPart.SendMessage("setRarity", rarity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
