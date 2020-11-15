using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CaveBlocs : MonoBehaviour
{
    public GameObject cave;
    public AudioClip discoverCaveSound;
    private Transform player;
    private bool discovered;
    private CompositeCollider2D collider;


    void Start()
    {
        discovered = false;
        collider = GetComponent<CompositeCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(Mathf.Abs(transform.position.y - player.position.y) > 2* 20.0f) { // 2* roomHeight. TODO USE CONSTANTS
            return;
        }

        if(collider.bounds.Contains(player.position))
         {
            if(!cave.activeSelf) {
                LevelManager.PauseGame = true;
                cave.SetActive(true);
                if(!discovered)
                {
                    SoundManager.instance.PlayAndMuteMusic(discoverCaveSound);
                }
            }

         }
         else {
            if(cave.activeSelf) {
                cave.SetActive(false);
                LevelManager.PauseGame = false;
                
            }
         }
    }
}