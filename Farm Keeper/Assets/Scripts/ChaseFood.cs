using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 10.0f;
    public float movementSpeed = 3.0f;
    public float rotationSpeed = 5.0f;

    private GameObject[] foods;
    private GameObject targetFood;  // nearest food
    private bool isFoodDetected = false;

    void Start()
    {
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
            isFoodDetected = true;
        else
            isFoodDetected = false;

        // Nearest food is in range
        if (targetFood != null && isFoodDetected)
        {
            // Move to food
            Vector3 direction = (targetFood.transform.position - transform.position).normalized;
            transform.position += direction * movementSpeed * Time.deltaTime;

            // Rotate to food
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Visualize detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
