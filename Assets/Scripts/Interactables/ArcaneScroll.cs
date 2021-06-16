using ED.Interactable;
using System.Linq;
using UnityEngine;

public class ArcaneScroll : MonoBehaviour, IInteractable
{
    public GameObject[] elementsParticles;
    public EElements ScrollElement;
    public enum EScrollTypes { Tutorial, Spell }
    public EScrollTypes scrollType;
    public bool completionCondition;
    [Header("Tutorial: (empty if scrollType = Spell)")]
    public Element elementToAdd;

    public InteractableDatas overviewDatas;
    public InteractableDatas OverviewDatas => this.overviewDatas;
    public Room LinkedRoom;

    private void OnEnable()
    {
        elementsParticles[(int)ScrollElement].SetActive(true);
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

            PlayerController.Instance.PlayerStats.Elements.ForEach(elem => elem.IsActive = false);
            PlayerController.Instance.PlayerStats.Elements.Single(elem => elem.Type == this.ScrollElement).IsActive = true;
            PlayerController.Instance.PlayerStats.ChangeElement(ScrollElement);
            if (completionCondition)
            {
                GameController.Instance.FireOnRoomComplete();
            }
        }
        LinkedRoom.RoomComplete();
    }
}
