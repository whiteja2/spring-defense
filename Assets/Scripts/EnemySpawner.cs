using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyWaypoint;

    public float spawnInterval = 2f; // Time interval between enemy spawns
    public float spawnRadius = 1f; // Maximum distance from the spawner where enemies can spawn

    private float spawnTimer = 0f;

    private void Update()
    {
        // Update the spawn timer
        spawnTimer += Time.deltaTime;

        // Check if it's time to spawn a new enemy
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f; // Reset the timer
        }
    }

    private void SpawnEnemy()
    {
        // Calculate a random position within the spawn radius
        Vector2 randomPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

        // Instantiate the enemy prefab at the random position
        // GameObject newEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        GameObject newEnemy = Instantiate(enemyPrefab, this.transform);

        
        EnemyBehavior enemyMovement = newEnemy.GetComponent<EnemyBehavior>();

        if (enemyMovement != null)
        {
            enemyMovement.enemyWaypoint = enemyWaypoint.transform;
        }
    }
}
