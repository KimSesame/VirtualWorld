using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public float decreaseHunger = 1.0f; // Decreasing amount of hunger gauge each interval
    public float hungerInterval = 1.0f;

    private SpawnManager spawnM;
    private HungerBar hungerBar;

    // Start is called before the first frame update
    void Start()
    {
        hungerBar = transform.Find("HungerBar").GetComponent<HungerBar>();
        spawnM = transform.parent.gameObject.GetComponent<SpawnManager>();

        // Feel Hunger
        InvokeRepeating(nameof(Hunger), 0, hungerInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (hungerBar.currentHunger <= 0)
            Die();
    }

    void Hunger()
    {
        hungerBar.currentHunger -= decreaseHunger;
    }

    void Die()
    {
        Destroy(gameObject);
        spawnM.animalCount--;
    }
}
