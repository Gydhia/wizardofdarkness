using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ED.Interactable
{
    public enum ETypeOfHoveredObject
    {
        Interactable,
        Talkable,
        Enemy
    }
    public interface IInteractable
    {
        InteractableDatas OverviewDatas { get; }

        void IsCompletionCondition();
        void Interact();
        void Hovered();
        void Unhovered();
    }
}

