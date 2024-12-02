using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public HealthBar healthBar; // Reference to the HealthBar script

    // Trigger-based collision detection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Using 'CompareTag' to compare tags
        {
            healthBar.TakeDamage(25); // Player loses 25 health when colliding with an enemy
        }
    }

    // Physics-based collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy")) // Using 'CompareTag' for consistency
        {
            healthBar.TakeDamage(25); // Player loses 25 health when colliding with an enemy
        }
    }
}
