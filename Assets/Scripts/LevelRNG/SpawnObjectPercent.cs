using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectPercent : SpawnObject
{
    [Range(0.0f, 1.0f)]
    public float percent = 1.0f;

    public new void Init()
    {
        if(Random.Range(0.0f, 1.0f) <= percent) {
            base.Init();
        }
    }

    void Start() {
        Init();
    }

}