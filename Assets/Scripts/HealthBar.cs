using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage; // Reference to the UI Image component representing the health bar

    // Using auto-properties for current and maximum health
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; } = 100f; // Set your max health here

    private void Start()
    {
        CurrentHealth = MaxHealth; // Initialize current health
        UpdateHealthBar(); // Update the health bar UI at start
    }

    // Method to set health directly
    public void SetHealth(float health)
    {
        CurrentHealth = Mathf.Clamp(health, 0, MaxHealth); // Ensure current health stays within limits
        UpdateHealthBar();
    }

    // Method to reduce health by a specific amount
    public void TakeDamage(float damage)
    {
        SetHealth(CurrentHealth - damage); // Use SetHealth to cap the health
    }

    // Method to update the health bar UI
    private void UpdateHealthBar()
    {
        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = CurrentHealth / MaxHealth; // Update the fill amount based on health
        }
    }

    // Repair method to increase health over time
    public IEnumerator RepairShip(float repairAmount)
    {
        float repairDuration = 2f; // Define repair duration
        float elapsed = 0f;

        while (elapsed < repairDuration)
        {
            CurrentHealth += repairAmount * (Time.deltaTime / repairDuration); // Increase health over time
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth); // Cap health to max
            UpdateHealthBar(); // Update the UI
            elapsed += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Repair complete!");
    }

    // Method to handle player death (optional, if applicable)
    private void Die()
    {
        Debug.Log("Player is dead!");
        EndGame(); // Call the method to end the game
    }

    private void EndGame()
    {
        // Implement game over logic here, like displaying a game over screen or restarting the level
        Debug.Log("Game Over!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
