using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCtrl : MonoBehaviour
{
    public void ExecuteGame()
    {
        InputManager.isPaused = false;
        SceneManager.LoadScene(sceneName: "GameScene", mode: LoadSceneMode.Single);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(sceneName: "MainMenu", mode: LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
