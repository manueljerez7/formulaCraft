using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class CarPartSpawnManager : MonoBehaviour
{
    [SerializeField] public CarPartSpawnPoint[] spawnPoints;
    private List<int> unusedSpawnPoints = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        //fill the list
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            unusedSpawnPoints.Add(i);
        }

        int chosenPointIndex;
        int chosenPoint;
        
        //spawn 6 wheels
        for (int i = 0; i < 6; i++)
        {
            //choose a random spawn point among the unused
            chosenPointIndex = Random.Range(0, unusedSpawnPoints.Count);
            chosenPoint = unusedSpawnPoints[chosenPointIndex];
            spawnPoints[chosenPoint].SendMessage("SpawnPart", 0, SendMessageOptions.RequireReceiver);
            //unusedSpawnPoints2.Remove(chosenPoint);
            unusedSpawnPoints.RemoveAt(chosenPointIndex);
            print("spawned a wheel");
        }
        
        //spawn 2 engines
        for (int i = 0; i < 2; i++)
        {
            chosenPointIndex = Random.Range(0, unusedSpawnPoints.Count);
            chosenPoint = unusedSpawnPoints[chosenPointIndex];
            spawnPoints[chosenPoint].SendMessage("SpawnPart", 1);
            unusedSpawnPoints.RemoveAt(chosenPointIndex);
            print("spawned an engine");
        }

        //spawn 2 brakes
        for (int i = 0; i < 2; i++)
        {
            chosenPointIndex = Random.Range(0, unusedSpawnPoints.Count);
            chosenPoint = unusedSpawnPoints[chosenPointIndex];
            spawnPoints[chosenPoint].SendMessage("SpawnPart", 2);
            unusedSpawnPoints.RemoveAt(chosenPointIndex);
            print("spawned a brake");
        }
        
        //spawn a random part on every leftover point
        foreach (var pointNum in unusedSpawnPoints)
        {
            int randomPartNum = Random.Range(0, 3);
            spawnPoints[pointNum].SendMessage("SpawnPart", randomPartNum);
            print("spawned a "+randomPartNum);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
