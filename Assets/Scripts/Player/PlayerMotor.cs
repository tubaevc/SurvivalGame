 using System;
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;
    private bool IsGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public bool crouching = false;
    public float crouchTimer = 1;
    public bool lerpCrouch = false;
    public bool sprinting = false;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        IsGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;
            if (crouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1, p);
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2, p);
            }

            if (p>1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
        
    }
    //receive the inputs for our InputManager.cs and apply them our char controller.
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;  //yatay hareket
        moveDirection.z = input.y;  //dikey hareketi ileri-geri harekete ceviriyoruz

        if (IsGrounded && playerVelocity.y<0)
        {
            playerVelocity.y = -2f;
        }
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);  // transform.TranformDirection karakterin ileri, geri, sağa veya sola doğru hareket etmesini sağlar.
        //controller.Move fonksiyonu kullanılarak karakterin hareket etmesi sağlanır.
        
        playerVelocity.y += gravity * Time.deltaTime;   
        controller.Move(playerVelocity * Time.deltaTime);  //karakterin dusmesini saglar
       // Debug.Log(playerVelocity.y);  //dusus hizi
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
        {
            speed = 8;
        }
        else
        {
            speed = 5;
        }
    }
}
