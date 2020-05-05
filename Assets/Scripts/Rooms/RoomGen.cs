using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGen : MonoBehaviour
{
    public SpawnObject[] lefts;
    public SpawnObject[] rights;
    public SpawnObject[] centers;
    public float percentage = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < lefts.Length; i++)
        {
            lefts[i].Init();
        }

        for(int i = 0; i < rights.Length; i++)
        {
            rights[i].Init();
        }

        for(int i = 0; i < centers.Length; i++)
        {
            centers[i].Init();
        }
        // todo instanciate object depending of each others
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
