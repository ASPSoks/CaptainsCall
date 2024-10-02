using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //declaração das variáveis que dá pra mudar na tela do unity
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _cannonballSpeed = 7f;

    [SerializeField] private float _fireCooldown = 1.5f;
    
    //declaração de mais variáveis
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private Animator _animator;
    public GameObject cannonballPrefab;

    public GameObject crossHair;

     private float _lastFireTime;
    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lastHorizontal = "LastHorizontal";
    private const string _lastVertical = "LastVertical";
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

//função que pega o aviso do input manager para acionar a função que dispara o canhão
    private void OnEnable(){
        InputManager.OnFire += FireCannon;
    }
    private void OnDisable() 
    {
        InputManager.OnFire -= FireCannon; 
    }
    private void Update()
    {
        //Movimentação do navio
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        _rb.velocity = _movement*_moveSpeed;
        _animator.SetFloat(_horizontal, _movement.x);
        _animator.SetFloat(_vertical, _movement.y);

        //if que faz com que o navio mantenha-se apontado para a última direção em que ele se movimentou
        if (_movement != Vector2.zero)
        {
            _animator.SetFloat(_lastHorizontal, _movement.x);
            _animator.SetFloat(_lastVertical, _movement.y);
        }
        //chama a função que atualiza a posição da mira
        UpdateCrossHairPosition();
    }
    //função que posiciona a mira onde o mouse está
    private void UpdateCrossHairPosition()
    {
       
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(InputManager.MousePosition);
        mouseWorldPosition.z = 0; 

        
        crossHair.transform.position = mouseWorldPosition;
    }
    //função de tiro
    private void FireCannon()
    {
        // Verifica se o cooldown terminou (se passou 1.5 segundos desde o último disparo)
        if (Time.time >= _lastFireTime + _fireCooldown)
        {
            // Converte a posição do mouse (screen space) para o espaço do mundo
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(InputManager.MousePosition);
            mouseWorldPosition.z = 0; // Para garantir que estamos no plano 2D

            // Calcula a direção da bola de canhão (do player até a mira)
            Vector2 direction = (mouseWorldPosition - transform.position).normalized;

            // Instancia a bola de canhão na posição do player
            GameObject cannonball = Instantiate(cannonballPrefab, transform.position, Quaternion.identity);

            // Define a velocidade da bola de canhão na direção do cursor
            cannonball.GetComponent<Rigidbody2D>().velocity = direction * _cannonballSpeed;

            // Atualiza o tempo do último disparo
            _lastFireTime = Time.time;
        }
    }
    
}
