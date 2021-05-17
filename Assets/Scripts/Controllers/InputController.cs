using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
    public float InteractRange; //The range of the raycast of objects we have to TOUCH with our HANDS. Ex: scrolls.
    public IInteractable HoveredItem = null;

    public Color DefaultColor;
    public Color[] HoveringColors;
    public Color ActualColor;
    
    IInteractable interactive;

    enum ETypeOfHoveredObject { Interactable, Talkable, Enemy }
    ETypeOfHoveredObject currentlyHovered;
    List<LayerMask> masks = new List<LayerMask>();
    private void Start()
    {
        foreach (int i in Enum.GetValues(typeof(ETypeOfHoveredObject)))
        {
            masks.Add(LayerMask.GetMask(((ETypeOfHoveredObject)i).ToString()));
        }
        ActualColor = DefaultColor;
    }
    
    private void Update()
    {
        ActualColor = DefaultColor;
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen


        // actual Ray
        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * InteractRange, Color.red);
        if (Physics.Raycast(ray, out hit, InteractRange, masks[(int)ETypeOfHoveredObject.Interactable]))
        {
            if (hit.collider.TryGetComponent(out interactive))
            {
                currentlyHovered = ETypeOfHoveredObject.Interactable;
                interactive.Hovered(true);
                //interactText.color = new Vector4(1, 1, 1, 1);
                if (Input.GetButtonDown("Interact"))
                {
                    interactive.Interact();
                }
            }
        }
        else
        {
            if (interactive != null)
            {
                interactive.Hovered(false);
                interactive = null;
                //interactText.color = new Vector4(1, 1, 1, 0);
            }
        }
        //if (isHovering) ActualColor = HoveringColors[(int)currentlyHovered];
        //cursor.color = ActualColor;
    }
}
