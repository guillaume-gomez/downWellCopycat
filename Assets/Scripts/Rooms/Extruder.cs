using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this component is called to remove bloc generated during the level
public class Extruder : MonoBehaviour
{
    [SerializeField]
    public String[] whiteListTag;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(Array.Exists(whiteListTag, item => item == col.gameObject.tag))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
