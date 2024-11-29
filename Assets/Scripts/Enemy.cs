using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float speed = 250f;
    public float chasingRadius = 250f;

    [SerializeField] private float _maxHealth = 100f;
    private float _health;
    public FloatingHealthBar healthBar;  // Referência à barra de vida

    private void Start()
    {
        _health = _maxHealth;
        healthBar.UpdateHealthBar(_health, _maxHealth); // Atualiza a barra de vida ao iniciar
    }

    private void FixedUpdate()
    {
        var position = transform.position;
        var playerPosition = player.transform.position;

        var playerDistance = Vector2.Distance(position, playerPosition);
        if (playerDistance < chasingRadius)
        {
            transform.position = Vector2.MoveTowards(position, playerPosition, speed * Time.fixedDeltaTime);
            return;
        }
        
        var currentNode = AStarManager.instance.FindNearestNode(position);
        var currentNodePosition = currentNode.transform.position;
        var gridDistance = Vector2.Distance(position, currentNodePosition);

        if (gridDistance > chasingRadius)
        {
            transform.position = Vector2.MoveTowards(position, currentNodePosition, speed * Time.fixedDeltaTime);
            return;
        }

        var playerNode = AStarManager.instance.FindNearestNode(playerPosition);
        var path = AStarManager.instance.GeneratePath(currentNode, playerNode);
        var nextNode = path[1];

        Debug.Log(nextNode.gameObject.name);
        transform.position = Vector2.MoveTowards(position, nextNode.transform.position, speed * Time.fixedDeltaTime);

        // A barra de vida será automaticamente atualizada conforme a vida muda
    }

    // Função que recebe dano
    public void TakeDamage(float damage)
    {
        _health -= damage;
        healthBar.UpdateHealthBar(_health, _maxHealth);  // Atualiza a barra de vida

        if (_health <= 0)
        {
            Die();
        }
    }

    // Função que destrói o inimigo quando a vida chega a 0
    private void Die()
    {
        Destroy(gameObject);
    }

    // Detecta colisões com as bolas de canhão
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cannonball"))
        {
            // Recebe dano da bola de canhão
            TakeDamage(25f); // Exemplo de dano de 25
            Destroy(other.gameObject); // Destrói a bola de canhão
        }
    }
}
