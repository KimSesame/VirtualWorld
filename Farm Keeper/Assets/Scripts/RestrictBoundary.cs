using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictBoundary : MonoBehaviour
{
    public float xRange = 20;
    public float zRange = 20;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Restrict object's boundary
        if (transform.position.x < -xRange) // left bound
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        if (transform.position.x > xRange) // right bound
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        if (transform.position.z < -zRange) // lower bound
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
        if (transform.position.z > zRange) // upper bound
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);

    }
}
