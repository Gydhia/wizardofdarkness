using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneOnEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManagementSystem.Instance.FireLoadSceneEvent();
        }
    }
}
