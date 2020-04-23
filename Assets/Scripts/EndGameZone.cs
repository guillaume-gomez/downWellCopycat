using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            GameManager.instance.WinLevel();
        }
    }
}
