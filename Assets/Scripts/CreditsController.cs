using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CreditsController : MonoBehaviour
{
    public void Update()
    {
        
    }
    public void BackToMenu()
    {
        SceneManagementSystem.Instance.actualSceneIndex = 2;
        SceneManagementSystem.Instance.SceneManagementSystem_LoadNextScene();
    }
}
