using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float speed = 10.0f;
    public float xRange = 10;
    public float zRange = 10;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Get the user's input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Move player
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);

        // Restrict player's boundary
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
