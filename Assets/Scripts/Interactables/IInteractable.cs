using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool InteractState { get; set; }

    void IsCompletionCondition();
    void Interact();
    void Hovered(bool isHovered);
    void Unhovered();
}
