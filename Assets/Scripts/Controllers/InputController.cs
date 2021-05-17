using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using ED.Interactable;

namespace ED.Controllers
{
    public class InputController : MonoBehaviour
    {
        public PlayerInput PlayerInputs;
        private PlayerBindingsMaps _playerBindingMap;

        public float InteractRange; // The range of the raycast of objects we have to TOUCH with our HANDS. Ex: scrolls.
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // Center of the screen
        public IInteractable HoveredItem = null;

        public Color DefaultColor;
        public Color[] HoveringColors;
        public Color ActualColor;

        List<LayerMask> masks = new List<LayerMask>();

        public static InputController Instance;
        private void Awake()
        {
            // Singleton pattern
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

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
            Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

            Debug.DrawRay(ray.origin, ray.direction * InteractRange, Color.red);

            if (Physics.Raycast(ray, out RaycastHit hit, InteractRange, masks[(int)ETypeOfHoveredObject.Interactable])) {
                if (hit.collider.TryGetComponent(out HoveredItem))
                {
                    HoveredItem.Hovered();
                    GameUIController.Instance.FireOnInteractOverviewChange(HoveredItem.OverviewDatas);
                    //interactText.color = new Vector4(1, 1, 1, 1);
                }
            } else {
                if (HoveredItem != null) 
                {
                    GameUIController.Instance.FireOnInteractOverviewCancel();
                    HoveredItem.Unhovered();
                    HoveredItem = null;
                    //interactText.color = new Vector4(1, 1, 1, 0);
                }
            }
            //if (isHovering) ActualColor = HoveringColors[(int)currentlyHovered];
            //cursor.color = ActualColor;
        }

        public void SwitchActionMap(PlayerBindings? map = null)
        {
            PlayerInputs.SwitchCurrentActionMap(map != null ? map.ToString() : _playerBindingMap.ToString());
        }
        public void SetupInputEvents()
        {
            this.PlayerInputs.actions[PlayerBindings.MousePosition.ToString()].performed += this.OnMouseMove;
            this.PlayerInputs.actions[PlayerBindings.LeftClick.ToString()].performed += this.OnLeftClick;
            this.PlayerInputs.actions[PlayerBindings.RightClick.ToString()].performed += this.OnRightClick;

            this.PlayerInputs.actions[PlayerBindings.Movements.ToString()].performed += this.OnMove;
            this.PlayerInputs.actions[PlayerBindings.Jump.ToString()].performed += this.OnJump;
            this.PlayerInputs.actions[PlayerBindings.Run.ToString()].performed += this.OnRun;

            this.PlayerInputs.actions[PlayerBindings.Interact.ToString()].performed += this.OnInteract;
            this.PlayerInputs.actions[PlayerBindings.FirstSpell.ToString()].performed += this.OnCastFirstSpell;
            this.PlayerInputs.actions[PlayerBindings.SecondSpell.ToString()].performed += this.OnCastSecondSpell;
            this.PlayerInputs.actions[PlayerBindings.ThirdSpell.ToString()].performed += this.OnCastThirdSpell;
            this.PlayerInputs.actions[PlayerBindings.FirstClass.ToString()].performed += this.OnChangeToFirstClass;
            this.PlayerInputs.actions[PlayerBindings.SecondClass.ToString()].performed += this.OnChangeToSecondClass;
            this.PlayerInputs.actions[PlayerBindings.ThirdClass.ToString()].performed += this.OnChangeToThirdClass;

            this.PlayerInputs.actions[PlayerBindings.Escape.ToString()].performed += this.OnEscape;
        }
        public void UnsetupInputEvents(InputAction.CallbackContext ctx)
        {
            this.PlayerInputs.actions[PlayerBindings.MousePosition.ToString()].performed -= this.OnMouseMove;
            this.PlayerInputs.actions[PlayerBindings.LeftClick.ToString()].performed -= this.OnLeftClick;
            this.PlayerInputs.actions[PlayerBindings.RightClick.ToString()].performed -= this.OnRightClick;

            this.PlayerInputs.actions[PlayerBindings.Movements.ToString()].performed -= this.OnMove;
            this.PlayerInputs.actions[PlayerBindings.Jump.ToString()].performed -= this.OnJump;
            this.PlayerInputs.actions[PlayerBindings.Run.ToString()].performed -= this.OnRun;

            this.PlayerInputs.actions[PlayerBindings.Interact.ToString()].performed -= this.OnInteract;
            this.PlayerInputs.actions[PlayerBindings.FirstSpell.ToString()].performed -= this.OnCastFirstSpell;
            this.PlayerInputs.actions[PlayerBindings.SecondSpell.ToString()].performed -= this.OnCastSecondSpell;
            this.PlayerInputs.actions[PlayerBindings.ThirdSpell.ToString()].performed -= this.OnCastThirdSpell;
            this.PlayerInputs.actions[PlayerBindings.FirstClass.ToString()].performed -= this.OnChangeToFirstClass;
            this.PlayerInputs.actions[PlayerBindings.SecondClass.ToString()].performed -= this.OnChangeToSecondClass;
            this.PlayerInputs.actions[PlayerBindings.ThirdClass.ToString()].performed -= this.OnChangeToThirdClass;

            this.PlayerInputs.actions[PlayerBindings.Escape.ToString()].performed -= this.OnEscape;
        }
        public void OnMouseMove(InputAction.CallbackContext ctx)
        {

        }
        public void OnLeftClick(InputAction.CallbackContext ctx)
        {
            if (PlayerController.Instance.PlayerStats.ActualElement.Skills[0].CanLaunch)
                PlayerController.Instance.PlayerStats.ActualElement.Skills[0].ActivatedSkill();
        }
        public void OnRightClick(InputAction.CallbackContext ctx)
        {
            if (PlayerController.Instance.PlayerStats.ActualElement.Skills[1].CanLaunch)
                PlayerController.Instance.PlayerStats.ActualElement.Skills[1].ActivatedSkill();
        }
        public void OnMove(InputAction.CallbackContext ctx)
        {

        }
        public void OnInteract(InputAction.CallbackContext ctx)
        {
            if (HoveredItem != null)
                HoveredItem.Interact();
        }
        public void OnCastFirstSpell(InputAction.CallbackContext ctx)
        {
            if (PlayerController.Instance.PlayerStats.ActualElement.Skills[2].CanLaunch)
                PlayerController.Instance.PlayerStats.ActualElement.Skills[2].ActivatedSkill();
        }
        public void OnCastSecondSpell(InputAction.CallbackContext ctx)
        {
            if (PlayerController.Instance.PlayerStats.ActualElement.Skills[3].CanLaunch)
                PlayerController.Instance.PlayerStats.ActualElement.Skills[3].ActivatedSkill();
        }
        public void OnCastThirdSpell(InputAction.CallbackContext ctx)
        {
            if (PlayerController.Instance.PlayerStats.ActualElement.Skills[4].CanLaunch)
                PlayerController.Instance.PlayerStats.ActualElement.Skills[4].ActivatedSkill();
        }
        public void OnChangeToFirstClass(InputAction.CallbackContext ctx)
        {
            PlayerController.Instance.PlayerStats.ChangeElement(EElements.Void);
        }
        public void OnChangeToSecondClass(InputAction.CallbackContext ctx)
        {
            PlayerController.Instance.PlayerStats.ChangeElement(EElements.Wind);
        }
        public void OnChangeToThirdClass(InputAction.CallbackContext ctx)
        {
            PlayerController.Instance.PlayerStats.ChangeElement(EElements.Earth);
        }
        public void OnJump(InputAction.CallbackContext ctx)
        {

        }
        public void OnRun(InputAction.CallbackContext ctx)
        {

        }
        public void OnEscape(InputAction.CallbackContext ctx)
        {

        }
    }

    public enum PlayerBindings
    {
        MousePosition,
        LeftClick,
        RightClick,
        Escape,

        Movements,
        Jump,
        Run,

        Interact,

        FirstSpell,
        SecondSpell,
        ThirdSpell,
        FirstClass,
        SecondClass,
        ThirdClass,
    }
    public enum PlayerBindingsMaps
    {
        Player,
        UI
    }
}

