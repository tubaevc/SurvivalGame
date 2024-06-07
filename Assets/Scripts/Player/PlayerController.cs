using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform playerCamera;
    public Animator animator;
    public Vector3 velocity;
    public Transform surfaceCheck;
    public LayerMask surfaceMask;

    public bool onSurface;
    public float speed = 2f;
    public float turnCalmTime = 0.1f;
    public float turnCalmVelocity;
    private float gravity = -9.81f;
    public float jumpRange = 1f;
    private float surfaceDistance = 0.4f;
    public float playerSprint = 4f;
    
     


    private void Start()
    {
        //controller = GetComponent<CharacterController>();
       // Cursor.lockState = CursorLockMode.Locked;

    }

    private void Update()
    {
        onSurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance, surfaceMask);
        if (onSurface && velocity.y<0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        Move();
        Jump();
        Sprint();
    }

    private void Move()
    {
        // Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        //
        //   if (direction.magnitude >= 0.1f)
        //   {
        //       float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg+ playerCamera.eulerAngles.y;
        //       float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity,
        //           turnCalmTime);
        //       transform.rotation = Quaternion.Euler(0f,angle,0f);
        //       Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        //       controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        //   }
        //   
          
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
    

          Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

          if (direction.magnitude >= 0.1f)
          {
              animator.SetBool("Idle",false);
              animator.SetBool("Walking", true);
              animator.SetBool("Running", false);
              animator.SetBool("RifleWalk", false);
              animator.SetBool("IdleAim", false);
              
              float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
              targetAngle = Mathf.Repeat(targetAngle, 360f); 
            //  Debug.Log("Target Angle: " + targetAngle);

              float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turnCalmTime);
             // Debug.Log("Smoothed Angle: " + angle);

              
              transform.rotation = Quaternion.Euler(0f, angle, 0f);

              Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
              //Debug.Log("Move Direction: " + moveDirection);

              controller.Move(moveDirection.normalized * speed * Time.deltaTime);
          }
          else
          {
              animator.SetBool("Idle",true);
              animator.SetBool("Walking", false);
              animator.SetBool("Running", false);
              // animator.SetBool("RifleWalk", true);
              // animator.SetBool("IdleAim", true);
          }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && onSurface)
        {
            animator.SetBool("Idle",false);
            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            animator.SetBool("Idle",true);
            animator.ResetTrigger("Jump");

        }
    }

    private void Sprint()
    {
        if (Input.GetButton("Sprint") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && onSurface)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                animator.SetBool("Walking", false);
                animator.SetBool("Running", true);
               
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                targetAngle = Mathf.Repeat(targetAngle, 360f);
                // Debug.Log("Target Angle: " + targetAngle);

                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity,
                    turnCalmTime);
                // Debug.Log("Smoothed Angle: " + angle);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                //Debug.Log("Move Direction: " + moveDirection);

                controller.Move(moveDirection.normalized * playerSprint * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Walking", true);
                animator.SetBool("Running", false);
            }
        }
    }

    //private void Update()
   // {
        

    //    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
     //   float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Yatay rotasyonu guncelle
     //   horizontalRotation += mouseX;
     //   verticalRotation -= mouseY;
     //   verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Yukarı ve aşağı rotasyonu sınırla
        //
        // if (Input.GetButtonDown("Jump") && isGrounded)
        // {
        //     velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        // }
        //  donme
     //   transform.localRotation = Quaternion.Euler(0, horizontalRotation, 0);
      //  Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        //  input al
     //   float moveForwardBackward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
    //    float moveLeftRight = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        // move
     //   Vector3 move = transform.right * moveLeftRight + transform.forward * moveForwardBackward;
     //   transform.position += move;
    }

