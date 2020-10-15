using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CaveBlocs : MonoBehaviour
{
    public GameObject cave;
    public AudioClip discoverCaveSound;
    private bool discovered;

    void Start()
    {
        discovered = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            LevelManager.PauseGame = true;
            cave.SetActive(true);
            if(!discovered)
            {
                SoundManager.instance.PlayAndMuteMusic(discoverCaveSound);
            }
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        Vector3 size = GetComponent<BoxCollider2D>().bounds.size;
        float absPositionX = Mathf.Abs(col.gameObject.transform.position.x);
        float positionAbs = Mathf.Abs(gameObject.transform.position.x);

        if( col.gameObject.name == "Player" &&
           (absPositionX < positionAbs ||
            absPositionX > positionAbs + size.x))
         {
            cave.SetActive(false);
            LevelManager.PauseGame = false;
         }
    }

}
