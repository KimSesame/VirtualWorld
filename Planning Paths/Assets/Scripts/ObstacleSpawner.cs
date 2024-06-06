using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public int[] obstacleXIndex;
    public int[] obstacleZIndex;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < obstacleXIndex.Length; i++)
            SpawnObstacle(obstacleXIndex[i], obstacleZIndex[i]);
    }

    private void SpawnObstacle(int x, int z)
    {
        int obstacleType = Random.Range(0, obstaclePrefabs.Length);

        GameObject newObstacle = Instantiate(obstaclePrefabs[obstacleType], new Vector3(x + GridGraph.inst.startCoordinate, 0, z + GridGraph.inst.startCoordinate), obstaclePrefabs[obstacleType].transform.rotation);
        newObstacle.transform.SetParent(this.transform);

        GridGraph.inst.RemoveNode(x, z);
    }
}
