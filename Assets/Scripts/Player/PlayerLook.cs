using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
  public Camera cam;
  private float xRotation = 0f;

  public float xSensitivity = 30f;
  public float ySensitivity = 30f;

  public void ProcessLook(Vector2 input)
  {
      float mouseX = input.x;
      float mouseY = input.y;
      //calculate cam rotation for looking up and down
      xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
      xRotation = Mathf.Clamp(xRotation, -90f, 90f);  // -90 ve 90 derece arasında sınırlayarak, kameranın fazla yukarı veya fazla asagi döomesini engeller.
      //apply to our camera 
      cam.transform.localRotation=Quaternion.Euler(xRotation,0,0);
      //rotate player to look left and right
      transform.Rotate(Vector3.up * (mouseX*Time.deltaTime)*xSensitivity);
  }
}
