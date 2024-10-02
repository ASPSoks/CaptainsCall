using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public static Vector2 Movement;
    public static Vector2 MousePosition;
    private PlayerInput _playerInput;
    private InputAction _moveAction;

    private InputAction _fireAction;

    private InputAction _aimHorizontal;
    private InputAction _aimVertical;
    public static event System.Action OnFire;
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _moveAction = _playerInput.actions["Mover"];

        _fireAction = _playerInput.actions["Fire"];

        _aimHorizontal = _playerInput.actions["AimHorizontal"];
        _aimVertical = _playerInput.actions["AimVertical"];
        _fireAction.performed += Fire;

    }
    private void Update(){
        Movement = _moveAction.ReadValue<Vector2>();
        MousePosition = new Vector2(_aimHorizontal.ReadValue<float>(), _aimVertical.ReadValue<float>());
    }

    private void Fire(InputAction.CallbackContext context){
        OnFire?.Invoke();
    }
}
   

 
