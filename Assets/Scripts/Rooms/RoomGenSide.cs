using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenSide : RoomGen
{
    // Start is called before the first frame update
    protected override void Start()
    {
        Init();

        SplitInChunkY(4, 0, spawnersSide, true);
        SplitInChunkY(4, 36, spawnersSide, false);
    }


}