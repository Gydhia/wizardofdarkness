using ED.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void LaunchGame()
    {
        InputController.Instance.UnsetupInputEvents();
        SceneManager.LoadScene("Tutorial");
    }
    public void SkipTutorial()
    {
        InputController.Instance.UnsetupInputEvents();
        SceneManager.LoadScene("DungeonScene");
    }
    public void Credits()
    {
        InputController.Instance.UnsetupInputEvents();
        SceneManager.LoadScene("Credits");
    }
    public void RelaunchScene()
    {
        InputController.Instance.UnsetupInputEvents();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackToMenu()
    {
        InputController.Instance.UnsetupInputEvents();
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
