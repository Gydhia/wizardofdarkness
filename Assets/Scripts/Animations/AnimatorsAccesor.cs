using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorsAccesor : MonoBehaviour
{
    public void FadeEnded()
    {
        PlayerUIManager.Instance.fadedOut = true;
        SceneManagementSystem.Instance.SceneManagementSystem_LoadNextScene();
    }
}
