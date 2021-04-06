using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speedSprint = 12f;
    public float speed = 12f;
    public float Currentspeed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float stamina = 100f;
    public float maxStamina = 100f;
    public float nb;
    public Slider slider;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    

  
    


    Vector3 velocity;
    bool isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        nb = speedSprint;
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            UseStamina(true);
        else
        {
            UseStamina(false);
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0) 
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
      
        controller.Move(move * Currentspeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) 
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
    public void UseStamina(bool isRunning)
    {
        if (isRunning)
        {
            UseStamina(Input.GetKey(KeyCode.LeftShift));
            Currentspeed = speedSprint;

            if (stamina > 0f)
                stamina -= 15f * Time.deltaTime;
            if (stamina <= 0f)
                speedSprint = speed;

            slider.value = stamina;
        }
        else
        {
            if (stamina <= maxStamina)
                stamina += 6f * Time.deltaTime;
            if (stamina <= 30f)
                stamina += 9f * Time.deltaTime;

            Currentspeed = speed;
            slider.value = stamina;
        }
    }


}
