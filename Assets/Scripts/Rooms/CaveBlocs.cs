using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CaveBlocs : MonoBehaviour
{
    public GameObject cave;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            LevelManager.PauseGame = true;
            cave.SetActive(true);
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
         {
            cave.SetActive(false);
            LevelManager.PauseGame = false;
         }
    }

}