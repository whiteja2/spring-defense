using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private string targetTag = "EnemyPoint"; // The tag of the target GameObject
    public float movementSpeed = 5f; // The speed at which the enemy moves towards the target
    public Transform enemyWaypoint;
    private GameObject target; // Reference to the target GameObject
    private bool hasReachedTarget = false; // Flag indicating whether the enemy has reached the target

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag);
    }

    private void Update()
    {
        if (!hasReachedTarget)
        {
            if (enemyWaypoint != null)
            {
                MoveTowardsWaypoint();
            }
        }
    }

    private void MoveTowardsWaypoint()
    {
        Vector2 direction = (enemyWaypoint.position - transform.position).normalized;
        Vector2 movement = direction * movementSpeed * Time.deltaTime;
        transform.Translate(movement);
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