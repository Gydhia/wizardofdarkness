using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public void ResetTrigger()
    {
        GetComponent<Animator>().ResetTrigger("Hit");
    }
}
