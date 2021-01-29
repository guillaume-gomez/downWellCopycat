using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEndLevel : RoomGen
{
    public SpawnObject[] centers;

    // Start is called before the first frame update
    protected override void Start()
    {
        for(int i = 0; i < centers.Length; i++)
        {
            if(centers[i].gameObject.activeSelf && Random.Range(0.0f, 1.0f) <= percentageCenter)
            {
                centers[i].Init();
            }
        }

    }

}
