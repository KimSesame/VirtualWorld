using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public int[] obstacleXIndex;
    public int[] obstacleZIndex;

    private GridGraph gridGraph;

    // Start is called before the first frame update
    void Start()
    {
        gridGraph = GameObject.Find("Grid Graph").GetComponent<GridGraph>();

        for (int i = 0; i < obstacleXIndex.Length; i++)
            SpawnObstacle(obstacleXIndex[i] + gridGraph.startCoordinate, obstacleZIndex[i] + gridGraph.startCoordinate);
    }

    private void SpawnObstacle(int x, int z)
    {
        int obstacleType = Random.Range(0, obstaclePrefabs.Length);

        GameObject newObstacle = Instantiate(obstaclePrefabs[obstacleType], new Vector3(x, 0, z), obstaclePrefabs[obstacleType].transform.rotation);
        newObstacle.transform.SetParent(this.transform);
    }
}
