using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class CarSpawnManagerTutorial : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawn()
    {
        int chosenPointIndex;
        int chosenPoint;

        //spawn 4 wheels
        for (int i = 0; i < 4; i++)
        {
            //choose a random spawn point among the unused
            chosenPointIndex = Random.Range(0, unusedSpawnPoints.Count);
            chosenPoint = unusedSpawnPoints[chosenPointIndex];
            spawnPoints[chosenPoint].SendMessage("SpawnPart", 0, SendMessageOptions.RequireReceiver);
            //unusedSpawnPoints2.Remove(chosenPoint);
            unusedSpawnPoints.RemoveAt(chosenPointIndex);
            print("spawned a wheel");
        }

        //spawn  engine
        chosenPointIndex = Random.Range(0, unusedSpawnPoints.Count);
        chosenPoint = unusedSpawnPoints[chosenPointIndex];
        spawnPoints[chosenPoint].SendMessage("SpawnPart", 1);
        unusedSpawnPoints.RemoveAt(chosenPointIndex);
        print("spawned an engine");

        //spawn brake

        chosenPointIndex = Random.Range(0, unusedSpawnPoints.Count);
        chosenPoint = unusedSpawnPoints[chosenPointIndex];
        spawnPoints[chosenPoint].SendMessage("SpawnPart", 2);
        unusedSpawnPoints.RemoveAt(chosenPointIndex);
        print("spawned a brake");
    }
        
}

