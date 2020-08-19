using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this component is called to remove bloc generated during the level
public class Extruder : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);
        Destroy(gameObject);
    }
}
