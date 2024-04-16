using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revival : MonoBehaviour
{
    public int yLimit = -30;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < yLimit)
        {
            // Reset physic state
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            // Reset position and rotation
            transform.position = new Vector3(-189, 0, 100);
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
