using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public string targetTag = "EnemyPoint"; // The tag of the target GameObject
    public float movementSpeed = 5f; // The speed at which the enemy moves towards the target

    private GameObject target; // Reference to the target GameObject
    private float speed = 0f;
    private bool hasReachedTarget = false; // Flag indicating whether the enemy has reached the target

    private void Start()
    {
        // Find the target GameObject using its tag
        target = GameObject.FindGameObjectWithTag(targetTag);

        // Ensure that a target is found
        if (target == null)
        {
            Debug.LogError("No target GameObject found with the specified tag!");
            enabled = false; // Disable the script to prevent movement
        }
        speed = Random.Range(1, movementSpeed);
    }

    private void Update()
    {
        // Check if the target GameObject is null
        if (target == null)
        {
            return; // Exit the Update method if there is no valid target
        }
        if (!hasReachedTarget)
        {
            // Calculate the direction towards the target
            Vector3 direction = target.transform.position - transform.position;

            // Normalize the direction to get a unit vector
            direction.Normalize();

            // Move the enemy towards the target
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            hasReachedTarget = true;
            Destroy(gameObject); // Destroy the enemy GameObject
        }
    }
}