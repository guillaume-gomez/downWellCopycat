using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CamerFollow : MonoBehaviour
{
    public Transform player;
    public float dampTime = 0.1f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    private bool shouldFollow = true;

    void Awake()
    {
        if(LevelManager.instance)
        {
            LevelManager.instance.OnWin += OnUnFollow;
        }
    }

    void Update()
    {
        if(!shouldFollow)
        {
            return;
        }

        Vector3 cameraPos = new Vector3(player.position.x, player.position.y, transform.position.z) + offset;
        transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref velocity, dampTime);
    }

    public void Unfollow()
    {
        //Debug.Log("Unfollow");
        shouldFollow = false;
    }

    private void OnUnFollow(object sender, System.EventArgs e)
    {
        Unfollow();
    }


}
