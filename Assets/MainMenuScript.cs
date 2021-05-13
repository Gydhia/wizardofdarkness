using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void LaunchGame()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
