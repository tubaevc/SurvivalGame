using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

   public Camera playerCamera;
   public bool isShooting, readyToShoot;
   private bool allowReset = true;
   public float shootingDelay = 2f;
   
   // burst 
   public int bulletsPerBurst = 3;
   public int burstBulletsLeft;
   
   //spread
   public float spreadIntensity;
   
   //bullet
   public GameObject bulletPrefab;
   public Transform bulletSpawn;
   public float bulletVelocity = 30;
   public float bulletPrefabLifeTime = 3f;

   public enum ShootingMode
   {
      Single,
      Burst,
      Auto
   }

   public ShootingMode currentShootingMode;

   private void Awake()
   {
      readyToShoot = true;
      burstBulletsLeft = bulletsPerBurst;
   }

   private void Update()
   {
      if (currentShootingMode == ShootingMode.Auto)
      {
         //holding down left button down
         isShooting = Input.GetKey(KeyCode.Mouse0);
      }
      else if (currentShootingMode==ShootingMode.Single || currentShootingMode==ShootingMode.Burst)
      {
         //click left button once
         isShooting = Input.GetKeyDown(KeyCode.Mouse0);
      }

      if (readyToShoot && isShooting)
      {
         burstBulletsLeft = bulletsPerBurst;
         FireWeapon();

      }
   }

   private void FireWeapon()
   {
      readyToShoot = false;
      Vector3 shootingDirection = CalculateDirectionandSpread().normalized;
      
      GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

      //pointing the bullet to face the shooting direction
      bullet.transform.forward = shootingDirection;
      //shoot the bullet
      bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized*bulletVelocity,ForceMode.Impulse); // mermi hareket
      //destroy the bullet - delay
      StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifeTime));
      
      //checking if we are done shooting
      if (allowReset)
      {
         Invoke("ResetShot",shootingDelay);
         allowReset = false;
      }

      if (currentShootingMode==ShootingMode.Burst && burstBulletsLeft > 1)
      {
         burstBulletsLeft--;
         Invoke("FireWeapon",shootingDelay);
      }
   }

   private void ResetShot()
   {
      readyToShoot = true;
      allowReset = true;
   }

   private Vector3 CalculateDirectionandSpread()
   {
      Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
      RaycastHit hit;
      Vector3 targetPoint;
      if (Physics.Raycast(ray,out hit))
      {
         targetPoint = hit.point;
      }
      else
      {
         targetPoint = ray.GetPoint(100);
      }

      Vector3 direction = targetPoint - bulletSpawn.position;
      float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
      float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

      return direction + new Vector3(x, y, 0);
   }

   private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
   {
      yield return new WaitForSeconds(delay);
      Destroy(bullet);
   }
}
