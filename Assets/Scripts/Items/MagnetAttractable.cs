using System;
using UnityEngine;

public class MagnetAttractable : MonoBehaviour {

     public float magnetStrength;
     public float magnetRange;
     private Transform attractedTo;

     private void Start ()
     {
         attractedTo = GameObject.FindGameObjectWithTag("Player").transform;
     }

     private void FixedUpdate ()
     {
        float distance = Vector3.Distance(attractedTo.position, transform.position);
        Vector3 direction = (attractedTo.position - transform.position).normalized;
        if(distance < magnetRange)
        {
            float forceFactor = magnetRange / distance;
            transform.position += direction * magnetStrength * forceFactor;
        }
     }
 }