using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; 
    public Vector3 offset; 
  //  public float rotationSpeed = 100f;
    void LateUpdate()
   {
     //   float horizontalRotation = Input.GetAxis("HorizontalCamera");

        // Rotate camera around the player
        // if (Mathf.Abs(horizontalRotation) > 0.1f)
        // {
        //     transform.RotateAround(player.position, Vector3.up, horizontalRotation * rotationSpeed * Time.deltaTime);
        // }

        // Follow the player
        //transform.LookAt(player.position);
        
        transform.position = player.position + offset;
    }
}
