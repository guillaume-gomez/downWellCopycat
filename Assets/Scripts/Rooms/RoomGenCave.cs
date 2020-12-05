using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenCave : MonoBehaviour
{
    public GameObject[] possibleCavePosition;
    void Start()
    {
        int rand = Random.Range(0, possibleCavePosition.Length);
        GameObject choosedObject = possibleCavePosition[rand];
        SpawnObject choosedObjectScript = choosedObject.GetComponent<SpawnObject>();
        Debug.Log(rand);
        choosedObjectScript.Init();
    }
}