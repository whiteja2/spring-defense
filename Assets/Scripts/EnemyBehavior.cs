using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyBehavior : MonoBehaviour
{
    private string targetTag = "EnemyPoint"; // The tag of the target GameObject
    public float movementSpeed = 5f; // The speed at which the enemy moves towards the target
    public Transform enemyWaypoint;
    private GameObject target; // Reference to the target GameObject
    private bool hasReachedTarget = false; // Flag indicating whether the enemy has reached the target
    public GameManager gameManager;
    public int rewardAmount = 10;

    private TMP_Text loseText;


    private void Start()
    {
        gameManager = GameManager.instance;
        target = GameObject.FindGameObjectWithTag(targetTag);
        GameObject textObject = GameObject.FindGameObjectWithTag("LoseText");
        if (textObject != null)
        {
            loseText = textObject.GetComponent<TMP_Text>();
        }
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
            if (loseText != null)
            {
                loseText.gameObject.SetActive(true);
                loseText.text = "You Suck!";
            }
        }
    }

    private void OnDestroy()
    {
        gameManager.UpdateCurrency(rewardAmount);
    }
}