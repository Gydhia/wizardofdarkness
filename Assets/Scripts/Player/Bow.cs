using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public LineRenderer _stringRenderer;
    public Transform ArrowPoint;
    public Transform BackArrowPoint;

    private void Update()
    {
        this.transform.rotation = Camera.main.transform.rotation;
    }
}
