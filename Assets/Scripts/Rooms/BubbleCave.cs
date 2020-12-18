using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BubbleCave : MonoBehaviour
{
    public AudioClip discoverCaveSound;
    private bool discovered;

    void Start()
    {
        discovered = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(!discovered)
            {
                SoundManager.instance.PlayAndMuteMusic(discoverCaveSound);
            }
        }
    }

}