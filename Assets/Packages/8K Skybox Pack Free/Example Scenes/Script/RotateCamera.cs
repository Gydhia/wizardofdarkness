using ED.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {
    float MouseRotateSpeed = 80f;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update () {
        float y = InputController.Instance.MouseSpeed.x * MouseRotateSpeed * Time.deltaTime;
        float x = InputController.Instance.MouseSpeed.y * MouseRotateSpeed * Time.deltaTime;
        transform.Rotate(-x, y, 0);

        Vector3 Angles = Camera.main.transform.eulerAngles;
        transform.eulerAngles = new Vector3(Angles.x, Angles.y, 0);
    }
}
