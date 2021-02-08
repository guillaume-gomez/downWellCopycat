using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;


public class PickTileGeneratePlatform : MonoBehaviour
{
    [Header("Percentages")]
    [Range(0, 1)]
    public float percentageBreakableBloc;
    [Space]
    [Header("Tiles")]
    public TileBase[] tiles;
    private GeneratePlatform generatePlatform;

    void Awake()
    {
       generatePlatform = GetComponent<GeneratePlatform>();
       PickUpTileBase();
    }

    void PickUpTileBase()
    {
        float random = Random.Range(0.0f, 1.0f);

        if(random <= percentageBreakableBloc) {
            generatePlatform.tile = tiles[0];
        } else {
            generatePlatform.tile = tiles[1];
        }
    }


}