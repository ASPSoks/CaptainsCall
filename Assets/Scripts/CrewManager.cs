using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewManager : MonoBehaviour
{
    public List<CrewMember> crewMembers; // List of all crew members on the ship
    public ShipGraph shipGraph; // Reference to the ship's graph system

    public ShipGraphNode crewPosition; // Current position of the crew (starting node)
    public HealthBar healthBar; // Reference to the health bar script
    public ParticleSystem repairParticles; // Reference to the particle system for repair
    public bool IsBusy = false;

    void Update()
    {
        // Trigger random damage repair with space
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            ShipGraphNode damageLocation = GetRandomDamageLocation(); // Get a random damage location
            List<Node> path = Dijkstra.FindShortestPath(crewPosition, damageLocation); // Find the shortest path
            
            if (path != null)
            {
                Debug.Log("Shortest path found!");
                MoveToDamageLocation(path);
            }
            else
            {
                Debug.Log("No path found!");
            }
        }
    }

    public ShipGraphNode GetRandomDamageLocation()
    {
        // Get a random damage location from the graph nodes
        int randomIndex = Random.Range(0, shipGraph.nodes.Count);
        
        // Return the random node directly since all are ShipGraphNode
        return shipGraph.nodes[randomIndex];
    }

    private void MoveToDamageLocation(List<Node> path)
    {
        // Here you would normally initiate movement to the target node
        StartRepair(); // Start the repair process once at the target node
    }

    public void StartRepair()
    {
        Debug.Log("Current Health: " + healthBar.CurrentHealth);
        Debug.Log("Max Health: " + healthBar.MaxHealth);
        
        // Use the public properties to access the health values
        if (!IsBusy && healthBar.CurrentHealth < healthBar.MaxHealth)
        {
            Debug.Log("Repair started");
            IsBusy = true;
            repairParticles.Play(); // Start shining effect
            StartCoroutine(RepairShip());
        }
        else
        {
            Debug.Log("Repair not needed or already in progress");
        }
    }

    private IEnumerator RepairShip()
    {
        float repairDuration = 2f; // Total duration of the repair
        float healthToRepair = 20f; // Amount of health to repair
        float elapsed = 0f;

        while (elapsed < repairDuration)
        {
            // Gradually fill the health bar
            healthBar.CurrentHealth += (healthToRepair / repairDuration) * Time.deltaTime;
            if (healthBar.CurrentHealth > healthBar.MaxHealth)
            {
                healthBar.CurrentHealth = healthBar.MaxHealth; // Cap the health to max
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        repairParticles.Stop(); // Stop the shining effect
        IsBusy = false;
        Debug.Log("Repair complete! Current Health: " + healthBar.CurrentHealth);
    }
}


public class CrewMember : MonoBehaviour
{
    public ShipGraphNode currentNode; // Posição atual do membro da tripulação
    public bool IsBusy = false;

    // You can remove the following if you don't need them
    public ParticleSystem movementParticles; // Efeito de partículas para movimento (not used)
    public ParticleSystem repairParticles; // Efeito de partículas para reparo (not used)

    // In this case, we won't need movement logic since you don't want visual representation.
}