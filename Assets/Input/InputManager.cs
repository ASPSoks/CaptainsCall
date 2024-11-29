using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static bool isPaused;
    public static Vector2 Movement;

    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _fireLeftAction;
    private InputAction _fireRightAction;
    private InputAction _pauseUnpause;
    public GameObject pauseScreen;

    public static event Action OnFireRight;
    public static event Action OnFireLeft;

    private void Awake()
    {
        // Link inputs from the Input System
        _playerInput = GetComponent<PlayerInput>();

        _moveAction = _playerInput.actions["Mover"];
        _fireLeftAction = _playerInput.actions["FireLeft"];
        _fireRightAction = _playerInput.actions["FireRight"];

        _pauseUnpause = _playerInput.actions["PauseUnpause"];
        pauseScreen.SetActive(false);

        // Link the input to the shooting functions
        _fireLeftAction.performed += FireLeft;
        _fireRightAction.performed += FireRight;
    }

    private void Update()
    {
        // Toggle pause/unpause when the assigned button is pressed
        if (_pauseUnpause.WasPerformedThisFrame())
        {
            isPaused = !isPaused;
            if (isPaused)
                Pause();
            else
                Unpause();
        }

        // Read movement input if the game is not paused
        if (!isPaused)
        {
            Movement = _moveAction.ReadValue<Vector2>();
        }
    }

    // Trigger the shooting event
    private void FireRight(InputAction.CallbackContext context)
    {
        if (isPaused) return;
        OnFireRight?.Invoke();
    }

    private void FireLeft(InputAction.CallbackContext context)
    {
        if (isPaused) return;
        OnFireLeft?.Invoke();
    }

    // Pause game
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0.0f;
        pauseScreen.SetActive(true);
    }

    // Unpause game
    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        pauseScreen.SetActive(false);
    }
}
