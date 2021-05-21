using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorsAccesor : MonoBehaviour
{
    public void FadeEnded()
    {
        SceneManagementSystem.Instance.SceneManagementSystem_LoadNextScene();
    }
}
