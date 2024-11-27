using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    AudioManager audioManager;
    public static bool isPaused;

    public static Vector2 Movement;
    public static Vector2 MousePosition;

    private PlayerInput _playerInput;

    private InputAction _moveAction;
    private InputAction _fireAction;
    private InputAction _aimHorizontal;
    private InputAction _aimVertical;

    private InputAction _pauseUnpause;
    [SerializeField] public GameObject pauseScreen;

    public static event System.Action OnFire;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        //atribui às variáveis os inputs designados no "Controller"(Input System)
        _playerInput = GetComponent<PlayerInput>();

        _moveAction = _playerInput.actions["Mover"];

        _fireAction = _playerInput.actions["Fire"];

        _aimHorizontal = _playerInput.actions["AimHorizontal"];
        _aimVertical = _playerInput.actions["AimVertical"];

        isPaused = false;
        _pauseUnpause = _playerInput.actions["PauseUnpause"];
        pauseScreen.SetActive(false);

        //Ativa a função de disparar quando recebe o input 
        _fireAction.performed += Fire;

    }
    private void Update(){
        isPaused = _pauseUnpause.WasPerformedThisFrame() ? !isPaused : isPaused;
        if (!isPaused)
        {
            Unpause();

        }
        else
        {
            Pause();
        }

        //movimentação do player e posição do mouse é lida;
        Movement = _moveAction.ReadValue<Vector2>();
        MousePosition = new Vector2(_aimHorizontal.ReadValue<float>(), _aimVertical.ReadValue<float>());
    }

    //Ativação da função de tiro no PlayerCtrl
    private void Fire(InputAction.CallbackContext context){
        if (isPaused) { return; }
        OnFire?.Invoke();
         audioManager.PlaySFX(audioManager.cannon);
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0.0f;
        pauseScreen.SetActive(true);
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        pauseScreen.SetActive(false);
    }
}
   

 
