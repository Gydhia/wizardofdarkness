using ED.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningConditions : MonoBehaviour
{
    public enum ConditionType { Interact, Eradicate}
    public ConditionType completionCondition;
    [Header("If Interact:")]
    [SerializeField]public IInteractable toInteractWith;

    private void Start()
    {
        if(completionCondition == ConditionType.Interact)
        {
            //toInteractWith.isCompletionCondition();
        }
    }
}
