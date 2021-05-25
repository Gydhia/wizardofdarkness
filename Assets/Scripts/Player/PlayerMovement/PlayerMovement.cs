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
            Debug.Log(MoveDirection.ToString());
            CharController.Move(MoveDirection * _actualSpeed * Time.deltaTime);

            Velocity.y += Gravity * Time.deltaTime;
            CharController.Move(Velocity * Time.deltaTime);
        }
    }

    public void PlayerJump()
    {
        if(isGrounded)
        Velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
    }

    public void UseStamina()
    {
        _walkSpeed = PlayerController.Instance.PlayerStats.MoveSpeed;
        float updatedStamina = 0f;
        
        if (stamina > 0f) {
            updatedStamina -= sprintingStaminaConsumption * Time.deltaTime;
            _actualSpeed = _walkSpeed * sprintFactor;
        } else {
            _actualSpeed = _walkSpeed;
        }

        stamina += updatedStamina;
        GameUIController.Instance.FireOnStaminaConsumed();
    }

    public void RegenerateStamina()
    {
        if(StaminaRegeneration == null)
            StartCoroutine(_regenerateStamina());
    }

    private IEnumerator _regenerateStamina()
    {
        _actualSpeed = _walkSpeed;

        if (stamina <= 30f)
            stamina += staminaCooldownRate * Time.deltaTime;
        else if (stamina < MaxStamina)
            stamina += depletedStaminaCooldownRate * Time.deltaTime;
        else
            yield break;

        yield return null;
    }

    public void Teleport(Vector3 pos)
    {
        CharController.enabled = false;
        CharController.transform.position = pos;
        CharController.enabled = true;
    }
}
