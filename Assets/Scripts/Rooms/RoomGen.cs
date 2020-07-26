﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGen : MonoBehaviour
{
    public SpawnObject[] lefts;
    public SpawnEnemy[] leftsEnemy;
    public SpawnObject[] rights;
    public SpawnEnemy[] rightsEnemy;
    public SpawnObject[] centers;
    [Range(0,1.0f)]
    public float percentageCenter = 1.0f;
    [Range(0,1.0f)]
    public float percentageSide = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < lefts.Length; i++)
        {
            if(lefts[i].gameObject.activeSelf && Random.Range(0.0f, 1.0f) <= percentageSide)
            {
                lefts[i].Init();
            }
            else if(
                i < leftsEnemy.Length &&
                leftsEnemy[i].gameObject.activeSelf && Random.Range(0.0f, 1.0f) <= percentageSide)
            {
                leftsEnemy[i].Init();
            }
        }

        for(int i = 0; i < rights.Length; i++)
        {
            if(rights[i].gameObject.activeSelf && Random.Range(0.0f, 1.0f) <= percentageSide)
            {
                rights[i].Init();
            }
            else if(
                i < rightsEnemy.Length &&
                rightsEnemy[i].gameObject.activeSelf && Random.Range(0.0f, 1.0f) <= percentageSide)
            {
                rightsEnemy[i].Init();
            }
        }

        for(int i = 0; i < centers.Length; i++)
        {
            if(centers[i].gameObject.activeSelf && Random.Range(0.0f, 1.0f) <= percentageCenter)
            {
                centers[i].Init();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
