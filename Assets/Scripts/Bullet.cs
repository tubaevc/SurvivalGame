using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
        private void OnCollisionEnter(Collision collision)
        {
            GameObject objectWeHit = collision.gameObject;

            // Debug mesajı ekleyerek hangi nesnenin vurulduğunu kontrol edin
            Debug.Log("Hit object: " + objectWeHit.name + " with tag: " + objectWeHit.tag);

            if (objectWeHit.CompareTag("Target"))
            {
                Debug.Log("Hit a target: " + objectWeHit.name + "!");
                CreateBulletImpactEffect(collision);
                Destroy(objectWeHit); // Hedefi yok et
                Destroy(gameObject); // Mermiyi yok et
            }
            else if (objectWeHit.CompareTag("Wall"))
            {
                Debug.Log("Hit a wall: " + objectWeHit.name + "!");
                CreateBulletImpactEffect(collision);
                Destroy(gameObject); // Mermiyi yok et
            }
            else
            {
                // Diğer çarpışmalar için bir debug mesajı ekleyin
                Debug.Log("Hit an object that is neither a target nor a wall: " + objectWeHit.name + " with tag: " + objectWeHit.tag);
            }
        }

   
        
    

    private void CreateBulletImpactEffect(Collision collision)
    {
        ContactPoint contact = collision.contacts[0]; //vurdugu ilk noktadan alıyor

        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffectPrefab,contact.point,Quaternion.LookRotation(contact.normal));
        
        hole.transform.SetParent(collision.gameObject.transform);
        
        
    }
}
