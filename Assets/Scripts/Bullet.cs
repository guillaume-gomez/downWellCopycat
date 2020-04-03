using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {   
        transform.Rotate(new Vector3(0,0,90));
        // if body does not collide, it destros itself after 2 secondes
        Destroy(gameObject, 2);
    }
}
