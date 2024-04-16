using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 50f;
    public float reachThreshold = 3f;
    public int waypointIndex = 0;

    private void Update()
    {
        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        Transform targetWaypoint = waypoints[waypointIndex];
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        transform.position += speed * Time.deltaTime * direction;
        transform.LookAt(targetWaypoint);

        // Move to next way point if reached
        if (Vector3.Distance(transform.position, targetWaypoint.position) < reachThreshold)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
        }
    }
}
