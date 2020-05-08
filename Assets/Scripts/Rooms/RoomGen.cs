using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGen : MonoBehaviour
{
    public SpawnObject[] lefts;
    public SpawnObject[] rights;
    public SpawnObject[] centers;
    [Range(0,1.0f)]
    public float percentage = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < lefts.Length; i++)
        {
            if(lefts[i].gameObject.activeSelf && Random.Range(0.0f, 1.0f) <= percentage)
            {

                lefts[i].Init();
            }
        }

        for(int i = 0; i < rights.Length; i++)
        {
            if(rights[i].gameObject.activeSelf && Random.Range(0.0f, 1.0f) <= percentage)
            {
                rights[i].Init();
            }
        }

        for(int i = 0; i < centers.Length; i++)
        {
            if(centers[i].gameObject.activeSelf && Random.Range(0.0f, 1.0f) <= percentage)
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
