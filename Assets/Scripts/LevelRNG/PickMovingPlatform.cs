using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;


public class PickMovingPlatform : MonoBehaviour
{
    public GameObject tiles;
    private GeneratePlatform generatePlatform;

    void Awake()
    {
       generatePlatform = GetComponent<GeneratePlatform>();
       PickUpPathType();
    }

    void PickUpPathType()
    {
        float random = Random.Range(0.0f, 1.0f);
        // swith enum pathType
    }


}