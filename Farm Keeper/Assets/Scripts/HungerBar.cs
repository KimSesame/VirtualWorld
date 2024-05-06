using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerBar : MonoBehaviour
{
    public float maxHunger = 100.0f;
    public float currentHunger;

    private Transform cam;
    private GameObject green;
    private GameObject red;

    void Awake()
    {
        cam = Camera.main.transform;
        green = transform.Find("Green").gameObject;
        red = transform.Find("Red").gameObject;
        currentHunger = 100.0f;
    }

    void Update()
    {
        // Hunger Bar always look at player
        transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.down);

        if (currentHunger > maxHunger)
            currentHunger = maxHunger;
    }

    void LateUpdate()
    {
        // Represent hunger gauge
        if (currentHunger == maxHunger)
            green.transform.localScale = new(1.5f, 0.125f, 0.125f);
        else
            green.transform.localScale = new(currentHunger / maxHunger * 1.5f, 0.1251f, 0.1251f);

        green.transform.localPosition = new((1.5f - green.transform.localScale.x) / 2f, 0, 0);
    }
}
