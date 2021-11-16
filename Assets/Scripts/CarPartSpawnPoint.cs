using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CarPartSpawnPoint : MonoBehaviour
{
    //rarity of the spawn point. Must be either 0 or 1.
    [FormerlySerializedAs("rarity")] [SerializeField] uint spawnPointRarity;

    public GameObject[] partPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        //instantiates a carPart of random type
        int random_part_num = Random.Range(0, 3);
        print("random_part_num = " + random_part_num);
        GameObject carPart = Instantiate(partPrefabs[random_part_num], transform.position, Quaternion.identity);
        
        //choose the rarity of the part, randomly chosen between 0 and 1 or between 1 and 2 depending on the rarity of the spawn point
        //based on a 30%-70% chance
        //(read GDD for more info)
        uint partRarity;
        if (Random.Range(0, 10) > 2)
            partRarity = spawnPointRarity;
        else
            partRarity = spawnPointRarity + 1;
        
        carPart.SendMessage("setRarity", partRarity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
