using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycastingSystem : MonoBehaviour
{
    public bool hovering;
    public Color hoveringColor;
    private Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0);

    public float talkingDistance;
    LayerMask dialogMask;

    private void Awake()
    {
        dialogMask = LayerMask.GetMask("Talkable");
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, talkingDistance, dialogMask))
        {
            hovering = true;
            if (Input.GetButtonDown("Interact"))
            {
                //StartDialog
            }
        }
        else
        {
            hovering = false;
        }
    }
}
