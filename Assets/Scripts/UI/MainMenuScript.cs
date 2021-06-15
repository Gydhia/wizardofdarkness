using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void LaunchGame()
    {
        GameUIController.Instance.FadeOut();
    }
    public void SkipTutorial()
    {
        SceneManager.LoadScene("DungeonScene");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void RelaunchScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
