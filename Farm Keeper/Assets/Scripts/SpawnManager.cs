using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public int animalCount = 1;
    public float increaseRate = 1.0f;

    private float spawnRangeX = 20;
    private float spawnRangeZ = 20;
    private float startDelay = 5.0f;
    private float spawnInterval = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Periodic Spawn
        InvokeRepeating(nameof(SpawnRandomAnimal), startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnRandomAnimal()
    {
        int cnt = animalCount;
        for (int i = 0; i < cnt * increaseRate; i++)
        {
            // Randomly generate animal index, spawn position and spawn rotation
            int animalIndex = Random.Range(0, animalPrefabs.Length);
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));
            Quaternion spawnRot = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            // Spawn
            GameObject newAnimal = Instantiate(animalPrefabs[animalIndex], spawnPos, spawnRot);
            newAnimal.transform.SetParent(this.transform);
            animalCount++;
        }
    }

    void CountAnimal()
    {

    }
}
