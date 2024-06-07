using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
   public Camera cam;
   public float giveDamageOf = 10f;
   public float shootingRange = 100f;
   public float fireCharge = 15f;
   private float nextTimeToShoot = 0f;
   public ParticleSystem muzzleSpark;
   public GameObject WoodedEffect;

   private int maximumAmmunition = 32;
   public int mag = 10;
   private int presentAmmunition;
   public float reloadingTime = 1.3f;
   private bool setReloading = false;

   public PlayerController player;
   public Transform hand;

   private void Awake()
   {
      transform.SetParent(hand);
      presentAmmunition = maximumAmmunition;
      
   }

   private void Update()
   {
      if (setReloading)
      {
         return;
      }

      if (presentAmmunition<=0)
      {
         StartCoroutine(Reload());
         return;
      }
      if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
      {
         nextTimeToShoot = Time.time + 1f / fireCharge;
         Shoot();
      }
   }

   private void Shoot()
   {
      if (mag==0)
      {
         return;
      }

      presentAmmunition--;

      if (presentAmmunition==0)
      {
         mag--;
      }
      
      
      muzzleSpark.Play();
      RaycastHit hitInfo;
      if (Physics.Raycast(cam.transform.position,cam.transform.forward,out hitInfo,shootingRange))
      {
         Debug.Log(hitInfo.transform.name);

         ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
         if (objectToHit != null)
         {
            objectToHit.ObjectHitDamage(giveDamageOf);
            GameObject WoodGo = Instantiate(WoodedEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(WoodGo,1f);
         }
      }
   }

   IEnumerator Reload()
   {
      player.speed = 0f;
      player.playerSprint = 0f;
      setReloading = true;
      Debug.Log("reloading...");
      yield return new WaitForSeconds(reloadingTime);
      presentAmmunition = maximumAmmunition;
      player.speed = 1.9f;
      player.playerSprint = 3;
      setReloading = false;
   }
}
