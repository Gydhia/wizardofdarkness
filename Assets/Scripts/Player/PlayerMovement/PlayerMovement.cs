using ED.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController CharController;

    public Vector3 Velocity;
    public Vector3 MoveDirection;
    private float _walkSpeed = 10f;
    public float sprintFactor = 2f;
    private float _actualSpeed = 1f;

    public float Gravity = -9.81f;
    public float JumpHeight = 3f;
    public float MaxStamina = 100f;
    [Range(0, 100)] public float stamina = 100f;
    
    public Transform groundCheck;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;
    
    public bool isGrounded;
    public bool CanMove = true;
    public bool IsRunning = false;
    public float sprintingStaminaConsumption = 15f;
    private float staminaCooldownRate = 9f;
    private float depletedStaminaCooldownRate = 6f;

    private Coroutine StaminaRegeneration;


    private void Awake()
    {
        CharController = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    public void Update()
    {
        if (CanMove)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && Velocity.y < 0)
                Velocity.y = -2f;
            Vector3 localDir = MoveDirection.x * transform.right + MoveDirection.z * transform.forward;
            CharController.Move(localDir * _actualSpeed * Time.deltaTime);

            Velocity.y += Gravity * Time.deltaTime;
            CharController.Move(Velocity * Time.deltaTime);
        }
        if (IsRunning)
        {
            _walkSpeed = PlayerController.Instance.PlayerStats.MoveSpeed;
            float updatedStamina = 0f;

            if (stamina > 0f)
            {
                updatedStamina -= sprintingStaminaConsumption * Time.deltaTime;
                _actualSpeed = _walkSpeed * sprintFactor;
            }
            else
            {
                _actualSpeed = _walkSpeed;
            }

            stamina += updatedStamina;
            GameUIController.Instance.FireOnStaminaChange();
        }
    }

    public void PlayerJump()
    {
        if (isGrounded)
            Velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
    }

    public void UnregenerateStamina()
    {
        if (StaminaRegeneration != null) {
            StopCoroutine(StaminaRegeneration);
            StaminaRegeneration = null;
        }
    }

    public void RegenerateStamina()
    {
        if(StaminaRegeneration == null)
            StaminaRegeneration = StartCoroutine(_regenerateStamina());
    }

    private IEnumerator _regenerateStamina()
    {
        _actualSpeed = _walkSpeed;

        while(stamina < MaxStamina)
        {
            if (stamina <= 30f)
            {
                stamina += staminaCooldownRate * Time.deltaTime;
                GameUIController.Instance.FireOnStaminaChange();
            }
            else if (stamina < MaxStamina)
            {
                stamina += depletedStaminaCooldownRate * Time.deltaTime;
                GameUIController.Instance.FireOnStaminaChange();
            }
            else
            {
                StaminaRegeneration = null;
                yield break;
            }
            yield return null;
        }
    }

    public void Teleport(Vector3 pos)
    {
        CharController.enabled = false;
        CharController.transform.position = pos;
        CharController.enabled = true;
    }
}
