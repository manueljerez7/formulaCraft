using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class CarPartSpawnManager : MonoBehaviour
{
    [SerializeField] public CarPartSpawnPoint[] spawnPoints;
    private HashSet<int> unusedSpawnPoints = new HashSet<int>();
    
    // Start is called before the first frame update
    void Start()
    {
        //fill the set
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            unusedSpawnPoints.Add(i);
        }
        
        print("unusedSpawnPoints = "+unusedSpawnPoints);

        int chosenPointNum;
        int chosenPoint;
        
        //spawn 4 wheels
        for (int i = 0; i < 4; i++)
        {
            //choose a random spawn point among the unused
            chosenPoint = Random.Range(0, unusedSpawnPoints.Count);
            //chosenPoint = unusedSpawnPoints
            spawnPoints[chosenPoint].SendMessage("SpawnPart", 1, SendMessageOptions.RequireReceiver);
            unusedSpawnPoints.Remove(chosenPoint);
            print("spawned type0 on spawn "+chosenPoint);
        }
        print("after wheels, unusedSpawnPoints = "+unusedSpawnPoints);
        
        //spawn engine
        chosenPoint = Random.Range(0, unusedSpawnPoints.Count);
        spawnPoints[chosenPoint].SendMessage("SpawnPart", 1);
        unusedSpawnPoints.Remove(chosenPoint);
        print("spawned type1 on spawn "+chosenPoint);
        print("after engine, unusedSpawnPoints = "+unusedSpawnPoints);
        
        //spawn brakes
        chosenPoint = Random.Range(0, unusedSpawnPoints.Count);
        spawnPoints[chosenPoint].SendMessage("SpawnPart", 2);
        unusedSpawnPoints.Remove(chosenPoint);
        print("spawned type2 on spawn "+chosenPoint);
        print("after brakes, unusedSpawnPoints = "+unusedSpawnPoints);
        
        //spawn a random part on every other point
        foreach (var pointNum in unusedSpawnPoints)
        {
            int randomPartNum = Random.Range(0, 3);
            spawnPoints[pointNum].SendMessage("SpawnPart", randomPartNum);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
