using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CaveBlocs : MonoBehaviour
{
    public GameObject cave;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("OnTriggerEnter2D " + col.gameObject.name);
        if(col.gameObject.name == "Player")
        {
            cave.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        cave.SetActive(false);
    }

}
