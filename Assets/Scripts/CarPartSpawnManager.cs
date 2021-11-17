using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class CarPartSpawnManager : MonoBehaviour
{
    [SerializeField] public CarPartSpawnPoint[] spawnPoints;
    private List<int> unusedSpawnPoints2 = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        //fill the list
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            unusedSpawnPoints2.Add(i);
        }

        print("unusedSpawnPoints = "+unusedSpawnPoints2.ToString());

        int chosenPointIndex;
        int chosenPoint;
        
        //spawn 4 wheels
        for (int i = 0; i < 4; i++)
        {
            //choose a random spawn point among the unused
            chosenPointIndex = Random.Range(0, unusedSpawnPoints2.Count);
            chosenPoint = unusedSpawnPoints2[chosenPointIndex];
            spawnPoints[chosenPoint].SendMessage("SpawnPart", 0, SendMessageOptions.RequireReceiver);
            //unusedSpawnPoints2.Remove(chosenPoint);
            unusedSpawnPoints2.RemoveAt(chosenPointIndex);
            print("spawned type0 on spawn "+chosenPoint);
        }
        print("after wheels, unusedSpawnPoints = "+unusedSpawnPoints2.ToString());
        
        //spawn engine
        chosenPointIndex = Random.Range(0, unusedSpawnPoints2.Count);
        chosenPoint = unusedSpawnPoints2[chosenPointIndex];
        spawnPoints[chosenPoint].SendMessage("SpawnPart", 1);
        unusedSpawnPoints2.RemoveAt(chosenPointIndex);
        print("spawned type1 on spawn "+chosenPoint);
        print("after engine, unusedSpawnPoints = "+unusedSpawnPoints2.ToString());
        
        //spawn brakes
        chosenPointIndex = Random.Range(0, unusedSpawnPoints2.Count);
        chosenPoint = unusedSpawnPoints2[chosenPointIndex];
        spawnPoints[chosenPoint].SendMessage("SpawnPart", 2);
        unusedSpawnPoints2.RemoveAt(chosenPointIndex);
        print("spawned type2 on spawn "+chosenPoint);
        print("after brakes, unusedSpawnPoints = "+unusedSpawnPoints2.ToString());
        
        //spawn a random part on every other point
        foreach (var pointNum in unusedSpawnPoints2)
        {
            int randomPartNum = Random.Range(0, 3);
            spawnPoints[pointNum].SendMessage("SpawnPart", randomPartNum);
            print("we visit spawnPoint " + pointNum + " on the final loop");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
