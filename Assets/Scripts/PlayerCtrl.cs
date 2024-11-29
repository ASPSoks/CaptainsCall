using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    private AudioManager _audioManager;
    private List<GameObject> _cannonballs;
    public List<Cannon> _leftSideCannons = new();
    public List<Cannon> _rightSideCannons = new();

    //declaração de mais variáveis
    private Vector2 _movement;
    public float _moveSpeed = 250f;
    public float _rotationSpeed = 150f;

    private Rigidbody2D _rb;
    public ContactFilter2D movementFilter;
    private readonly List<RaycastHit2D> _hits = new();



    private void Awake()
    {
        _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        _cannonballs = new List<GameObject>();
        _rb = GetComponent<Rigidbody2D>();
    }

    //função que pega o aviso do input manager para acionar a função que dispara o canhão
    private void OnEnable()
    {
        InputManager.OnFireRight += FireRight;
        InputManager.OnFireLeft += FireLeft;
    }
    private void OnDisable()
    {
        InputManager.OnFireRight -= FireRight;
        InputManager.OnFireLeft -= FireLeft;
    }
    private void FixedUpdate()
    {
        if (InputManager.isPaused) { return; }

        _hits.Clear();
        int count = _rb.Cast(_movement, movementFilter, _hits.ToArray(), _moveSpeed * Time.fixedDeltaTime);
        if (count == 0)
        {
            MovePlayer();
        }

    }

    private void MovePlayer()
    {
        //Movimentação do navio
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        _rb.rotation -= _movement.x * _rotationSpeed * Time.fixedDeltaTime;
        _rb.velocity = _movement.y * _moveSpeed * transform.up;

        foreach (var cannon in _leftSideCannons)
        {
            cannon.MoveCannon(_rb.velocity);
        }

        foreach (var cannon in _rightSideCannons)
        {
            cannon.MoveCannon(_rb.velocity);
        }
    }

    private void FireRight()
    {
        FireCannons(_rightSideCannons, new Vector2(transform.up.y, -transform.up.x));
        _audioManager.PlaySFX(_audioManager.cannon);
    }

    private void FireLeft()
    {
        FireCannons(_leftSideCannons, new Vector2(-transform.up.y, transform.up.x));
        _audioManager.PlaySFX(_audioManager.cannon);
    }

    //função de tiro
    private void FireCannons(List<Cannon> selectedCannons, Vector2 shootingDirection)
    {
        foreach (var cannon in selectedCannons.Where(c => c.IsAvailableToShoot()))
        {
            GameObject cannonball = cannon.FireCannon(shootingDirection);
            if (cannonball != null)
            {
                _cannonballs.Add(cannonball);
            }
        }
    }
}
