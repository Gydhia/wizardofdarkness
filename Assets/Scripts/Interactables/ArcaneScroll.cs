using ED.Interactable;
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

    public InteractableDatas overviewDatas;
    public InteractableDatas OverviewDatas => this.overviewDatas;

    private void OnEnable()
    {
        elementsParticles[(int)scrollElement].SetActive(true);
    }

    public void IsCompletionCondition()
    {
        return;
    }
    public void Hovered()
    {
        return;
    }

    public void Unhovered()
    {
        return;
    }

    public void Interact()
    {
        if (scrollType == EScrollTypes.Tutorial)
        {
            TextingSystemManager.Instance.NextLine();
            Destroy(gameObject);

            PlayerController.Instance.PlayerStats.Elements.Add(elementToAdd);
            elementToAdd.Init();
            PlayerController.Instance.PlayerStats.ChangeElement(scrollElement);
            if (completionCondition)
            {
                GameController.Instance.FireOnRoomComplete();
            }
        }
    }
}
