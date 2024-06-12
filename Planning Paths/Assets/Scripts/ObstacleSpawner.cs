using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public static event Action OnObstacleSpawned;

    public GameObject[] obstaclePrefabs;
    public int[] obstacleXIndex;
    public int[] obstacleZIndex;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn obstacles according to map
        for (int i = 0; i < obstacleXIndex.Length; i++)
            SpawnObstacle(obstacleXIndex[i], obstacleZIndex[i]);
        OnObstacleSpawned?.Invoke();
    }

    private void SpawnObstacle(int x, int z)
    {
        // Randomize obstacle type
        int obstacleType = UnityEngine.Random.Range(0, obstaclePrefabs.Length);

        // Set obstacle
        GameObject newObstacle = Instantiate(obstaclePrefabs[obstacleType], new Vector3(x + GridGraph.inst.startCoordinate, 0, z + GridGraph.inst.startCoordinate), obstaclePrefabs[obstacleType].transform.rotation);
        newObstacle.transform.SetParent(this.transform);

        // Remove node from graph
        GridGraph.inst.RemoveNode(x, z);
    }
}
