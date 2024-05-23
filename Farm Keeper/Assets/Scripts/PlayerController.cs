using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject food;
    public float horizontalInput;
    public float verticalInput;
    public float speed = 15.0f;
    public float rotationSpeed = 300.0f;
    public float xRange = 20;
    public float zRange = 20;

    private CameraSwitcher cameraSwitcher;

    // Start is called before the first frame update
    void Start()
    {
        cameraSwitcher = FindObjectOfType<CameraSwitcher>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the user's input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        ViewType type = (ViewType)cameraSwitcher.GetViewType();
        Turn(type);
        Move(type);

        // Drop the food
        if (Input.GetKeyDown(KeyCode.Space))
            Feed();
    }

    void Turn(ViewType type)
    {
        if (type == ViewType.Top)
        {
            // Only update rotation if there is input
            if (horizontalInput != 0 || verticalInput != 0)
            {
                float targetAngle = 0f;

                // Decide target angle with inputs
                if (horizontalInput > 0 && verticalInput > 0) // Up & Right
                    targetAngle = 45f;
                else if (horizontalInput < 0 && verticalInput > 0) // Up & Left
                    targetAngle = 315f;
                else if (horizontalInput > 0 && verticalInput < 0) // Down & Right
                    targetAngle = 135f;
                else if (horizontalInput < 0 && verticalInput < 0) // Down & Left
                    targetAngle = 225f;
                else if (horizontalInput > 0) // Right
                    targetAngle = 90f;
                else if (horizontalInput < 0) // Left
                    targetAngle = 270f;
                else if (verticalInput > 0) // Up
                    targetAngle = 0f;
                else if (verticalInput < 0) // Down
                    targetAngle = 180f;

                // Turn to target angle
                Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else if (type == ViewType.FirstPerson)
        {
            transform.Rotate(Vector3.up, horizontalInput * Time.deltaTime * rotationSpeed / 2);
        }
    }

    void Move(ViewType type)
    {
        // Move player
        if (type == ViewType.Top)
        {
            transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed, Space.World);
            transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed, Space.World);
        }
        else if (type == ViewType.FirstPerson)
        {
            transform.Translate(transform.forward * verticalInput * Time.deltaTime * speed, Space.World);
        }
    }

    void Feed()
    {
        Instantiate(food, transform.position, food.transform.rotation);
    }
}