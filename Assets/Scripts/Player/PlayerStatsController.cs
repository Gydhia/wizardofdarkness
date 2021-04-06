using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerStats.Instance.ChangeElement(PlayerStats.EElements.Void);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerStats.Instance.ChangeElement(PlayerStats.EElements.Wind);
            Debug.Log("ag");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerStats.Instance.ChangeElement(PlayerStats.EElements.Earth);
        }
    }
}
