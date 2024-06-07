using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
   public GameObject AimCam;
   public GameObject AimCanvas;
   public GameObject ThirdPersonCam;
   public GameObject ThirdPersonCanvas;
   public Animator animator;
   private void Update()
   {
      if (Input.GetButton("Fire2") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
      {
         animator.SetBool("Idle", false);
         animator.SetBool("IdleAim", true);
         animator.SetBool("RifleWalk", true);
         animator.SetBool("Walking", true);
         
         ThirdPersonCam.SetActive(false);
         ThirdPersonCanvas.SetActive(false);
         AimCam.SetActive(true);
         AimCanvas.SetActive(true);
      }
      else if (Input.GetButton("Fire2"))
      {
         animator.SetBool("Idle", false);
         animator.SetBool("IdleAim", true);
         animator.SetBool("RifleWalk", true);
         animator.SetBool("Walking", false);
         
         ThirdPersonCam.SetActive(false);
         ThirdPersonCanvas.SetActive(false);
         AimCam.SetActive(true);
         AimCanvas.SetActive(true);
      }
      else
      {
         animator.SetBool("Idle", true);
         animator.SetBool("IdleAim", false);
         animator.SetBool("RifleWalk", false);

         
         ThirdPersonCam.SetActive(true);
         ThirdPersonCanvas.SetActive(true);
         AimCam.SetActive(false);
         AimCanvas.SetActive(false);
      }
   }
}
