using UnityEngine;

public class ArcaneScroll : MonoBehaviour, IInteractable
{
    public GameObject[] elementsParticles;
    public EElements scrollElement;
    public enum EScrollTypes { Tutorial, Spell }
    public EScrollTypes scrollType;
    public bool completionCondition;
    [Header("Tutorial: (empty if scrollType = Spell)")]
    public Element elementToAdd;

    public bool InteractState { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void OnEnable()
    {
        elementsParticles[(int)scrollElement].SetActive(true);
    }
    public void Interact()
    {
        if (scrollType == EScrollTypes.Tutorial)
        {
            TextingSystemManager.Instance.NextLine();
            Destroy(gameObject);
            if (!PlayerUIManager.Instance.hudActive) PlayerUIManager.Instance.ToggleHud(true);

            PlayerStats.Instance.elements.Add(elementToAdd);
            elementToAdd.Init();
            PlayerStats.Instance.ChangeElement(scrollElement);
            if (completionCondition)
            {
                GameController.Instance.FireOnRoomComplete();
            }
        }
    }

    public void Hovered(bool isHovered)
    {
        Debug.Log("Need to implement Hovering Scrolls -> need a crosshair");
    }

    public void IsCompletionCondition()
    {
        completionCondition = true;
    }

    public void Unhovered()
    {
        throw new System.NotImplementedException();
    }
}
