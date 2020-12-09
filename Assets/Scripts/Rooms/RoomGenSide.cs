using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenSide : RoomGen
{
    // Start is called before the first frame update
    void Start()
    {
        Init();

        SplitInChunkY(4, 0, spawnersSide, true);
        SplitInChunkY(4, 36, spawnersSide, false);
    }

    protected override void CreateGenericBloc(float xPosition, float yPosition, GameObject[] spawners, int index, PlatformPosition platformPosition)
    {
        GameObject obj = CreateSpwaner(xPosition, yPosition, spawners, index);
        obj.GetComponent<SpawnObject>().isLeft = platformPosition == PlatformPosition.Left;
        obj.GetComponent<SpawnObject>().Init();

    }

}