using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    public Camera cam;
    public ParticleSystem muzzleSpark;
    public GameObject WoodedEffect;
    public PlayerController player;
    public Transform hand;
    public Animator animator;

    public float giveDamageOf = 10f;
    public float shootingRange = 100f;
    public float fireCharge = 15f;
    private float nextTimeToShoot = 0f;

    private int maximumAmmunition = 32;
    public int mag = 10;
    private int presentAmmunition;
    public float reloadingTime = 1.3f;
    private bool setReloading = false;


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

        if (presentAmmunition <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            animator.SetBool("Fire", true);
            animator.SetBool("Idle", false);
            nextTimeToShoot = Time.time + 1f / fireCharge;
            Shoot();
        }
        else if (Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Firewalk", true);
        }
        else if (Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            animator.SetBool("Idle", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("Firewalk", true);
            animator.SetBool("Walking", true);
            animator.SetBool("Reloading", false);
        }
        else
        {
            animator.SetBool("Fire", false);
            animator.SetBool("Idle", true);
            animator.SetBool("Firewalk", false);
        }
    }

    private void Shoot()
    {
        if (mag == 0)
        {
            return;
        }

        presentAmmunition--;

        if (presentAmmunition == 0)
        {
            mag--;
        }


        muzzleSpark.Play();
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);

            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
            if (objectToHit != null)
            {
                objectToHit.ObjectHitDamage(giveDamageOf);
                GameObject WoodGo = Instantiate(WoodedEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(WoodGo, 1f);
            }
        }
    }

    IEnumerator Reload()
    {
        player.speed = 0f;
        player.playerSprint = 0f;
        setReloading = true;
        Debug.Log("reloading...");
        animator.SetBool("Reloading",true);
        yield return new WaitForSeconds(reloadingTime);
        animator.SetBool("Reloading",false);

        presentAmmunition = maximumAmmunition;
        player.speed = 1.9f;
        player.playerSprint = 3;
        setReloading = false;
    }
}