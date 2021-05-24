using ED.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 1.5f;

    public Transform playerBody;

    private float _xCamRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }



    void LateUpdate()
    {
        Vector2 lookInput = InputController.Instance.MouseSpeed;
        
        if (lookInput != Vector2.zero)
        {

            _xCamRotation += lookInput.y * 0.2f;
            _xCamRotation = Mathf.Clamp(_xCamRotation, -90f, 90f);
         
            transform.localRotation = Quaternion.Euler(-_xCamRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity);
        }
        
    }
}
