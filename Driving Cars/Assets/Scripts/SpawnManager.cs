using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] vehiclePrefabs;

    private float spawnRangeX = 7;
    private float spawnPosZ = 160;
    private float startDelay = 2;
    private float spawnInterval = 5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnOncomingVehicle", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnOncomingVehicle()
    {
        // Randomly generate vehicle index and spawn position
        Vector3 spawnPos = new(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
        int vehicleIndex = Random.Range(0, vehiclePrefabs.Length);
        Instantiate(vehiclePrefabs[vehicleIndex], spawnPos, vehiclePrefabs[vehicleIndex].transform.rotation);
    }
}
