using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidCollision : MonoBehaviour
{
    public float raycastDistance = 2.0f;

    private LayerMask collisionLayer = 7;
    private Movement movement;

    void Start()
    {
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        RaycastHit hit; // to predict collision

        // Collision with other animals predicted
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastDistance, collisionLayer))
        {
            // RayCast visualization
            float hitDistance = Vector3.Distance(transform.position, hit.point);
            Debug.DrawRay(transform.position, transform.forward * hitDistance, Color.red);

            // Avoid collision by moving other direction
            movement.isStrolling = false;
            movement.movementSpeed = movement.initMovementSpeed * 0.1f;
            movement.rotationSpeed = movement.initRotationSpeed * 0.1f;
            movement.Stroll();
        }
        else
        {
            // RayCast visualization
            Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green);
        }
    }
}
