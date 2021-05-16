using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{

    private void Update()
    {
        if (PlayerStats.Instance.elements.Count > 2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PlayerStats.Instance.ChangeElement(EElements.Earth);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PlayerStats.Instance.ChangeElement(EElements.Wind);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PlayerStats.Instance.ChangeElement(EElements.Void);
            }
        }
        else if (PlayerStats.Instance.elements.Count > 1)
        {

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PlayerStats.Instance.ChangeElement(EElements.Wind);
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PlayerStats.Instance.ChangeElement(EElements.Void);
            }
        }
        else if (PlayerStats.Instance.elements.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PlayerStats.Instance.ChangeElement(EElements.Void);
            }
        }
    }
}
