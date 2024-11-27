using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    AudioManager audioManager;
    [SerializeField] private float _damage = 25f;    // Dano que a bola de canhão vai causar
    [SerializeField] private float _lifetime = 5f;   // Tempo de vida da bola de canhão antes de desaparecer
    private void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        // Destroi a bola de canhão após o tempo definido em _lifetime
        Destroy(gameObject, _lifetime);
    }

    // Função chamada ao colidir com outro objeto
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto com o qual colidiu é um inimigo
        if (other.CompareTag("Enemy"))
        {
            // Acessa o componente Enemy do objeto atingido
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                // Aplica o dano ao inimigo
                enemy.TakeDamage(_damage);
                audioManager.PlaySFX(audioManager.damage);
            }

            // Destrói a bola de canhão após o impacto
            Destroy(gameObject);
        }
    }
}
