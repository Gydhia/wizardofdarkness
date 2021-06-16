using ED.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    public bool isPausable;
    public GameObject PausePanel;

    public bool gameIsPaused;

    private void Awake()
    {
        Instance = this;
    }
    public void ToggleMenu()
    {
        if (gameIsPaused)
        {
            gameIsPaused = false;
            Time.timeScale = 1f;
            PausePanel.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            gameIsPaused = true;
            Time.timeScale = 0f;
            PausePanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        InputController.Instance.UnsetupInputEvents();
        SceneManager.LoadScene("MainMenu");
    }
    public void Reload()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        InputController.Instance.UnsetupInputEvents();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
