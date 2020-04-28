using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenCave : MonoBehaviour
{
    public GameObject[] possibleCavePosition;
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, possibleCavePosition.Length);
        GameObject choosedObject = possibleCavePosition[rand];
        SpawnObject choosedObjectScript = choosedObject.GetComponent<SpawnObject>();

        choosedObjectScript.noSpawn = true;
        choosedObjectScript.Init();
    }

}