using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController CharController;

    public float sprintFactor;
    private float _walkSpeed;
    private float _actualSpeed;
    public float Gravity = -9.81f;
    public float JumpHeight = 3f;
    public float MaxStamina = 100f;
    [Range(0, 100)] public float stamina = 100f;
    public Vector3 move;
    public Transform groundCheck;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;
    public Vector3 velocity;
    public bool isGrounded;
    public bool canMove = true;
    public float airControl;
    float control;
    public float sprintingStaminaConsumption = 15f;
    private float staminaCooldownRate = 9f;
    private float depletedStaminaCooldownRate = 6f;


    private void Awake()
    {
        CharController = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    public void Update()
    {
        if (canMove)
        {
            #region stamina
            UseStamina(Input.GetKey(KeyCode.LeftShift));
            #endregion
            #region Jumping and Gravity
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0)
                velocity.y = -2f;
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            }
            #endregion
            #region move
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            #region Animation
            //if (PlayerAnimationState.Instance != null && PlayerAnimationState.Instance.playerAnimator != null)
            //{
            //    PlayerAnimationState.Instance.playerAnimator.SetFloat("DirX", x);
            //    PlayerAnimationState.Instance.playerAnimator.SetFloat("DirZ", z);
            //    PlayerAnimationState.Instance.playerAnimator.SetBool("isMoving", (move == Vector3.zero));
            //    if (!PlayerAnimationState.Instance.playerAnimator.GetBool("isJumping"))
            //        PlayerAnimationState.Instance.playerAnimator.SetBool("isJumping", !isGrounded);
            //    else
            //        PlayerAnimationState.Instance.playerAnimator.SetBool("isJumping", false);
            //}
            #endregion
            move = transform.right * x  + transform.forward * z ;
            CharController.Move(move * _actualSpeed * Time.deltaTime);
            velocity.y += Gravity * Time.deltaTime;
            CharController.Move(velocity * Time.deltaTime);
            #endregion

        }
    }

    public void UseStamina(bool isRunning)
    {
        _walkSpeed = PlayerController.Instance.PlayerStats.MoveSpeed;
        float updatedStamina = 0f;

        if (isRunning)
        {
            if (stamina > 0f) {
                updatedStamina -= sprintingStaminaConsumption * Time.deltaTime;
                _actualSpeed = _walkSpeed * sprintFactor;
            } else {
                _actualSpeed = _walkSpeed;
            }
        }
        else
        {
            if (stamina <= 30f)
                updatedStamina += staminaCooldownRate * Time.deltaTime;

            else if (stamina < MaxStamina)
                updatedStamina += depletedStaminaCooldownRate * Time.deltaTime;
            
            _actualSpeed = _walkSpeed;
        }

        stamina += updatedStamina;
        GameUIController.Instance.FireOnStaminaConsumed();
    }

    public void Teleport(Vector3 pos)
    {
        CharController.enabled = false;
        CharController.transform.position = pos;
        CharController.enabled = true;
    }
}
