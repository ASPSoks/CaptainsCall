using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import this namespace to work with UI components
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage; // Reference to the UI Image component representing the health bar
    private float maxHealth; // The maximum health value
    private float currentHealth; // The current health value

    private void Start()
    {
        // Initialize health values at the start
        InitializeHealth(100f); // You can set this to any value you like or pass it from another script
    }

    // Initialize health values
    public void InitializeHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Method to update the health value
    public void SetHealth(float health)
    {
        currentHealth = health;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth; // Ensure current health doesn't exceed max health
        UpdateHealthBar();
    }

    // Method to reduce health by a specific amount
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthBar();

        if (currentHealth == 0)
        {
            Die(); // Call the death method if health is zero
        }
    }

    // Method to update the health bar UI
    private void UpdateHealthBar()
    {
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = currentHealth / maxHealth; // Update the fill amount based on health
        }
    }

    // Method to handle player death
    private void Die()
    {
        Debug.Log("Player is dead!"); 
        EndGame(); // Call the method to end the game
    }
    private void EndGame()
    {
        // Implement game over logic here, like displaying a game over screen or restarting the level
        Debug.Log("Game Over!"); 
        // For example, you could reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}