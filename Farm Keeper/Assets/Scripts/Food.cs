using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float increaseHunger = 10.0f;    // Increasing amount of hunger guage when animals eat food

    // Update is called once per frame
    void Update()
    {

    }

    // Food meet Animal -> destroy itself and increase animal's hunger guage
    void OnTriggerEnter(Collider other)
    {
        // Only animals are able to eat foods
        if (!other.gameObject.CompareTag("Animal"))
            return;

        HungerBar hungerBar = other.transform.Find("HungerBar").GetComponent<HungerBar>();

        hungerBar.currentHunger += increaseHunger;
        Destroy(gameObject);
    }
}
