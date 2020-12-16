using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

// this component is called to remove bloc generated during the level
public class Extruder : MonoBehaviour
{
    private Tilemap tilemap;

    void Start()
    {
        tilemap = GameObject.Find("Borders").GetComponent<Tilemap>();
        if(tilemap)
        {
            Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
            //Debug.Log(cellPosition);
            Debug.Log(tilemap.GetTile(cellPosition));
            tilemap.SetTile(cellPosition, null);
            //Destroy(gameObject);
        }
    }
}
