using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private float speed = 4f; // Movement speed of the enemy
    [SerializeField] private float detectionRange = 10f; // Distance at which the enemy detects the player
    [SerializeField] private GameObject deathEffect; // Effect to spawn when the enemy dies

    private NavMeshAgent agent; // AI pathfinding component
    private Transform player; // Reference to the player
    private bool isPlayerDetected = false; // Whether the enemy detects the player

    private void Awake()
    {
        // Get the NavMeshAgent component and set its speed
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = speed;
        }
    }

    private void Start()
    {
        // Automatically find the player using the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        HandleChasing(); // Continuously check for chasing behavior
    }

    /// <summary>
    /// Moves the enemy toward the player if they are detected.
    /// </summary>
    private void HandleChasing()
    {
        if (isPlayerDetected && player != null && isActiveAndEnabled)
        {
            agent.SetDestination(player.position); // Move toward the player's position
        }
    }

    /// <summary>
    /// Called when an object stays inside the trigger collider.
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = true; // Start chasing the player
        }
    }

    /// <summary>
    /// Called when an object exits the trigger collider.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = false; // Stop chasing the player
        }
    }

    /// <summary>
    /// Called when an object enters the trigger collider.
    /// If hit by a bullet, the enemy will die.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Die();
        }
    }

    /// <summary>
    /// Handles the enemy's death process.
    /// </summary>
    private void Die()
    {
        // Spawn the death effect if available
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Start the coroutine before disabling the object
        StartCoroutine(DestroyAfterDelay(2f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for 2 seconds
        gameObject.SetActive(false); // Hide the enemy after delay
        Destroy(gameObject); // Destroy the enemy object
    }

}
