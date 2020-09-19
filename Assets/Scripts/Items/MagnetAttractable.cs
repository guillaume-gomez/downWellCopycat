using System;
using UnityEngine;

public class MagnetAttractable : MonoBehaviour {

     public float magnetStrength;
     public float magnetRange;

     private Rigidbody2D ownRigidbody;
     private Transform attractedTo;

     private void Start ()
     {
         ownRigidbody = GetComponent<Rigidbody2D>();
     }

     private void OnTriggerEnter2D (Collider2D other)
     {
         if (other.gameObject.tag == "Player")
         {
             attractedTo = other.transform;
         }
     }

     private void OnTriggerExit2D (Collider2D other)
     {
         if (other.gameObject.tag == "Player")
         {
             attractedTo = null;
         }
     }

     private void FixedUpdate ()
     {
         if (attractedTo != null)
         {
            Vector3 direction = (attractedTo.position - transform.position).normalized;
            /*float forceFactor = 1.0f - (direction / magnetRange); // magnetism is actually inversely proportional to the square of distance, but this is good enough approximation
            if (forceFactor > 0)
            {
                ownRigidbody.AddForce(direction * magnetStrength, ForceMode2D.Force);
            }*/
         }
     }
 }