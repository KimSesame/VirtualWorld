using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float detectionRange = 10.0f;
    public float initMovementSpeed;
    public float initRotationSpeed;
    public float movementSpeed;
    public float rotationSpeed = 5.0f;
    public bool isFoodDetected = false;
    public bool isStrolling = false;

    private GameObject[] foods;
    private GameObject targetFood;  // nearest food
    private RestrictBoundary restrictBound;
    private Vector3 strollPoint;

    void Start()
    {
        restrictBound = GetComponent<RestrictBoundary>();
        initMovementSpeed = movementSpeed;
        initRotationSpeed = rotationSpeed;
    }

    void Update()
    {
        SearchFood();

        // Move to nearest food if the food is in range
        if (targetFood != null && isFoodDetected)
            ChaseFood();
        // IF NOT, just stroll
        else
            Stroll();
    }

    void SearchFood()
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
    }

    void ChaseFood()
    {
        // Move to food
        Vector3 direction = (targetFood.transform.position - transform.position).normalized;
        transform.position += direction * movementSpeed * 1.3f * Time.deltaTime;

        // Rotate to food
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void Stroll()
    {
        // Set new destination
        if (!isStrolling)
            strollPoint = new(Random.Range(-restrictBound.xRange, restrictBound.xRange), 0f, Random.Range(-restrictBound.zRange, restrictBound.zRange));

        Vector3 direction = (strollPoint - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Move & Rotate
        transform.position += direction * movementSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, strollPoint) < 1.0f)
            isStrolling = false;
        else
            isStrolling = true;

        movementSpeed = initMovementSpeed;
        rotationSpeed = initRotationSpeed;
    }

    void OnDrawGizmosSelected()
    {
        // Visualize detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
