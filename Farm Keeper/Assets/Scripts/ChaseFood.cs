using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChaseFood : MonoBehaviour
{
    public float detectionRange = 10.0f;
    public float movementSpeed = 3.0f;
    public float rotationSpeed = 5.0f;

    private GameObject[] foods;
    private GameObject targetFood;  // nearest food
    private RestrictBoundary restrictBound;
    private bool isFoodDetected = false;
    private bool isStrolling = false;
    private Vector3 strollPoint;

    void Start()
    {
        restrictBound = GetComponent<RestrictBoundary>();
    }

    void Update()
    {
        // Find all foods
        foods = GameObject.FindGameObjectsWithTag("Food");

        // Find nearest food
        float minDistance = Mathf.Infinity;
        foreach (GameObject food in foods)
        {
            float distance = Vector3.Distance(transform.position, food.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                targetFood = food;
            }
        }

        // Set food detection flag
        if (minDistance <= detectionRange)
        {
            isFoodDetected = true;
            isStrolling = false;
        }
        else
            isFoodDetected = false;

        // Nearest food is in range
        if (targetFood != null && isFoodDetected)
        {
            // Move to food
            Vector3 direction = (targetFood.transform.position - transform.position).normalized;
            transform.position += direction * movementSpeed * 1.3f * Time.deltaTime;

            // Rotate to food
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        // Nearset food is NOT in range
        else
        {
            if (!isStrolling)
                strollPoint = new(Random.Range(-restrictBound.xRange, restrictBound.xRange), 0f, Random.Range(-restrictBound.zRange, restrictBound.zRange));

            Vector3 direction = (strollPoint - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.position += direction * movementSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, strollPoint) < 1.0f)
                isStrolling = false;
            else
                isStrolling = true;
        }
    }

    void Stroll()
    {
        Vector3 direction = (strollPoint - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.position += direction * movementSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }

    void OnDrawGizmosSelected()
    {
        // Visualize detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
