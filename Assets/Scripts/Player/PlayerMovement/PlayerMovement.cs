using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public static PlayerMovement Instance;
    public event SetSlider UpdateStamina;
    public float sprintFactor;
    private float walkSpeed;
    private float actualSpeed;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float maxStamina = 100f;
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
    CharacterController charController;
    public float sprintingStaminaConsumption = 15f;
    private float staminaCooldownRate = 9f;
    private float depletedStaminaCooldownRate = 6f;


    private void Awake()
    {
        Instance = this;
        charController = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    public void Update()
    {
        if (canMove)
        {
            #region stamina
            UseStamina(Input.GetKey(KeyCode.LeftShift));
            UpdateStamina?.Invoke(stamina);
            #endregion
            #region Jumping and Gravity
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0)
                velocity.y = -2f;
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            #endregion
            #region move
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            #region Animation

            PlayerAnimationState.Instance.playerAnimator.SetFloat("DirX", x);
            PlayerAnimationState.Instance.playerAnimator.SetFloat("DirZ", z);
            PlayerAnimationState.Instance.playerAnimator.SetBool("isMoving", (move == Vector3.zero? false:true));
            if(!PlayerAnimationState.Instance.playerAnimator.GetBool("isJumping"))
                PlayerAnimationState.Instance.playerAnimator.SetBool("isJumping", !isGrounded);
            else
                PlayerAnimationState.Instance.playerAnimator.SetBool("isJumping", false);
            #endregion
            move = transform.right * x  + transform.forward * z ;
            controller.Move(move * actualSpeed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            #endregion

        }
    }

    public void UseStamina(bool isRunning)
    {
        walkSpeed = PlayerStats.Instance.moveSpeed;
        if (isRunning)
        {
            if (stamina > 0f)
            {
                stamina -= sprintingStaminaConsumption * Time.deltaTime;
                actualSpeed = walkSpeed*sprintFactor;
            }
            else
            {
                actualSpeed = walkSpeed;
            }
        }
        else
        {
            if (stamina <= 30f)
            {
                stamina += staminaCooldownRate * Time.deltaTime;
            }

            else if (stamina < maxStamina)
            {
                stamina += depletedStaminaCooldownRate * Time.deltaTime;
            }
            actualSpeed = walkSpeed;
        }
    }

    public void Teleport(Vector3 pos)
    {
        charController.enabled = false;
        charController.transform.position = pos;
        charController.enabled = true;
    }
}
