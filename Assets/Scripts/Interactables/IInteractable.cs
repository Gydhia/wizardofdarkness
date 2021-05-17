using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void isCompletionCondition();
    void Interact();
    void Hovered(bool isHovered);
}
