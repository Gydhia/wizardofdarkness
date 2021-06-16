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

        public Vector2 MouseSpeed = Vector2.zero;
        public Vector2 PlayerAxisSpeed = Vector2.zero;

        public Keyboard Keyboard;

        public float InteractRange = 10f; // The range of the raycast of objects we have to TOUCH with our HANDS. Ex: scrolls.
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // Center of the screen
        public IInteractable HoveredItem = null;

        public Color DefaultColor;
        public Color[] HoveringColors;
        public Color ActualColor;

        public List<LayerMask> Masks = new List<LayerMask>();

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
            Keyboard = InputSystem.GetDevice<Keyboard>();

            SetupInputEvents();

            ActualColor = DefaultColor;
        }

        //private void OnDestroy()
        //{
        //    UnsetupInputEvents();
        //}

        private void Update()
        {
            if(Camera.main != null)
            {
                Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

                Debug.DrawRay(ray.origin, ray.direction * InteractRange, Color.red);

                if (Physics.Raycast(ray, out RaycastHit hit, InteractRange, Masks[(int)ETypeOfHoveredObject.Interactable]))
                {
                    if (hit.collider.TryGetComponent(out HoveredItem))
                    {
                        HoveredItem.Hovered();
                        GameUIController.Instance.FireOnInteractOverviewChange(HoveredItem.OverviewDatas);
                    }
                }
                else
                {
                    if (HoveredItem != null)
                    {
                        GameUIController.Instance.FireOnInteractOverviewCancel();
                        HoveredItem.Unhovered();
                        HoveredItem = null;
                    }
                }
                //if (isHovering) ActualColor = HoveringColors[(int)currentlyHovered];
                //cursor.color = ActualColor;
            }

        }

        public void SwitchActionMap(PlayerBindings? map = null)
        {
            PlayerInputs.SwitchCurrentActionMap(map != null ? map.ToString() : _playerBindingMap.ToString());
        }
        public void SetupInputEvents()
        {
            this.PlayerInputs.actions[PlayerBindings.MousePosition.ToString()].performed += this.OnMouseMove;
            this.PlayerInputs.actions[PlayerBindings.LeftClick.ToString()].performed += this.OnLeftClick;
            this.PlayerInputs.actions[PlayerBindings.LeftClickRelease.ToString()].performed += this.OnLeftClickRelease;
            this.PlayerInputs.actions[PlayerBindings.RightClick.ToString()].performed += this.OnRightClick;

            this.PlayerInputs.actions[PlayerBindings.Movements.ToString()].performed += this.OnMove;
            this.PlayerInputs.actions[PlayerBindings.Jump.ToString()].performed += this.OnJump;
            this.PlayerInputs.actions[PlayerBindings.Run.ToString()].performed += this.OnRun;
            this.PlayerInputs.actions[PlayerBindings.Run.ToString()].canceled += this.OnStopRun;

            this.PlayerInputs.actions[PlayerBindings.Interact.ToString()].performed += this.OnInteract;
            this.PlayerInputs.actions[PlayerBindings.FirstSpell.ToString()].performed += this.OnCastFirstSpell;
            this.PlayerInputs.actions[PlayerBindings.FirstSpellRelease.ToString()].performed += this.OnReleaseFirstSpell;
            this.PlayerInputs.actions[PlayerBindings.SecondSpell.ToString()].performed += this.OnCastSecondSpell;
            this.PlayerInputs.actions[PlayerBindings.ThirdSpell.ToString()].performed += this.OnCastThirdSpell;
            this.PlayerInputs.actions[PlayerBindings.FirstClass.ToString()].performed += this.OnChangeToFirstClass;
            this.PlayerInputs.actions[PlayerBindings.SecondClass.ToString()].performed += this.OnChangeToSecondClass;
            this.PlayerInputs.actions[PlayerBindings.ThirdClass.ToString()].performed += this.OnChangeToThirdClass;

            this.PlayerInputs.actions[PlayerBindings.Escape.ToString()].performed += this.OnEscape;
        }
        public void UnsetupInputEvents()
        {
            this.PlayerInputs.actions[PlayerBindings.MousePosition.ToString()].performed -= this.OnMouseMove;
            this.PlayerInputs.actions[PlayerBindings.LeftClick.ToString()].performed -= this.OnLeftClick;
            this.PlayerInputs.actions[PlayerBindings.LeftClickRelease.ToString()].performed -= this.OnLeftClickRelease;
            this.PlayerInputs.actions[PlayerBindings.RightClick.ToString()].performed -= this.OnRightClick;

            this.PlayerInputs.actions[PlayerBindings.Movements.ToString()].performed -= this.OnMove;
            this.PlayerInputs.actions[PlayerBindings.Jump.ToString()].performed -= this.OnJump;
            this.PlayerInputs.actions[PlayerBindings.Run.ToString()].performed -= this.OnRun;
            this.PlayerInputs.actions[PlayerBindings.Run.ToString()].canceled -= this.OnStopRun;

            this.PlayerInputs.actions[PlayerBindings.Interact.ToString()].performed -= this.OnInteract;
            this.PlayerInputs.actions[PlayerBindings.FirstSpell.ToString()].performed -= this.OnCastFirstSpell;
            this.PlayerInputs.actions[PlayerBindings.FirstSpellRelease.ToString()].performed -= this.OnReleaseFirstSpell;
            this.PlayerInputs.actions[PlayerBindings.SecondSpell.ToString()].performed -= this.OnCastSecondSpell;
            this.PlayerInputs.actions[PlayerBindings.ThirdSpell.ToString()].performed -= this.OnCastThirdSpell;
            this.PlayerInputs.actions[PlayerBindings.FirstClass.ToString()].performed -= this.OnChangeToFirstClass;
            this.PlayerInputs.actions[PlayerBindings.SecondClass.ToString()].performed -= this.OnChangeToSecondClass;
            this.PlayerInputs.actions[PlayerBindings.ThirdClass.ToString()].performed -= this.OnChangeToThirdClass;

            this.PlayerInputs.actions[PlayerBindings.Escape.ToString()].performed -= this.OnEscape;
        }
        public void OnMouseMove(InputAction.CallbackContext ctx)
        {
            MouseSpeed = ctx.ReadValue<Vector2>();
        }
        public void OnLeftClick(InputAction.CallbackContext ctx)
        {
            _castSpell(0, true);
        }
        public void OnLeftClickRelease(InputAction.CallbackContext ctx)
        {
            _castSpell(0, false);
        }
        public void OnRightClick(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
                _castSpell(1, true);
            else if (ctx.canceled)
                _castSpell(1, false);
        }
        public void OnMove(InputAction.CallbackContext ctx)
        {
            Vector2 speed = ctx.ReadValue<Vector2>();

            PlayerController.Instance.PlayerMovement.MoveDirection = new Vector3(speed.x, 0f, speed.y);
        }
        public void OnInteract(InputAction.CallbackContext ctx)
        {
            if (HoveredItem != null)
                HoveredItem.Interact();
        }
        public void OnCastFirstSpell(InputAction.CallbackContext ctx)
        {
            _castSpell(2, true);   
        }
        public void OnReleaseFirstSpell(InputAction.CallbackContext ctx)
        {
            _castSpell(2, false);
        }
        public void OnCastSecondSpell(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
                _castSpell(3, true);
            else if (ctx.canceled)
                _castSpell(3, false);
        }
        public void OnCastThirdSpell(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
                _castSpell(4, true);
            else if (ctx.canceled)
                _castSpell(4, false);
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
            PlayerController.Instance.PlayerMovement.PlayerJump();
        }
        public void OnRun(InputAction.CallbackContext ctx)
        {
            if (ctx.performed) {
                PlayerController.Instance.PlayerMovement.UnregenerateStamina();
                PlayerController.Instance.PlayerMovement.IsRunning = true;
            }   
        }
        public void OnStopRun(InputAction.CallbackContext ctx)
        {
            PlayerController.Instance.PlayerMovement.IsRunning = false;
            PlayerController.Instance.PlayerMovement.RegenerateStamina();   //Nouvelle feature : bravo
        }
        public void OnEscape(InputAction.CallbackContext ctx)
        {
            if(PauseMenu.Instance != null)
            {
                if (PauseMenu.Instance.isPausable)
                {
                    PauseMenu.Instance.ToggleMenu();
                }
            }
        }

        private void _castSpell(int index, bool performed)
        {
            if (performed) {
                if (PlayerController.Instance.PlayerStats.ActualElement.Skills[index].CanLaunch)
                    PlayerController.Instance.PlayerStats.ActualElement.Skills[index].ActivatedSkill();
            }
            else {
                PlayerController.Instance.PlayerStats.ActualElement.Skills[index].HasReleased = true;
            }
        }
    }

    public enum PlayerBindings
    {
        MousePosition,
        LeftClick,
        LeftClickRelease,
        RightClick,
        Escape,

        Movements,
        Jump,
        Run,

        Interact,

        FirstSpell,
        FirstSpellRelease,
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

