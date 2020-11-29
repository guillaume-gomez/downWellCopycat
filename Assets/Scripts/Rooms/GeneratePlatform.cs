using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public enum PlatformPosition{ Left, Right, Center }

public class GeneratePlatform : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase tile;
    public Vector3Int offset; 
    public PlatformPosition platformPosition;
    [Space]
    [Header("X values")]
    [Range(1, 24)]
    public int xRangeMin;
    [Range(1, 24)]
    public int xRangeMax;

    [Space]
    [Header("Y values")]
    [Range(1, 18)]
    public int yRangeMin;
    [Range(1, 18)]
    public int yRangeMax;

    void Start()
    {

        tilemap =  GameObject.Find("Platforms").GetComponent<Tilemap>();
        int xChoosed = Random.Range(xRangeMin, xRangeMax);
        int yChoosed = Random.Range(yRangeMin, yRangeMax);

        for(int x = 0; x < xChoosed; x++)
        {
            for(int y = 0; y < yChoosed; y++)
            {
                //Vector3Int cellPosition = tilemap.WorldToCell(transform.position);
                Vector3Int cellPosition = new Vector3Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), 0);
                Vector3Int tilePosition = new Vector3Int(x,y,0);
                switch(platformPosition)
                {
                    case PlatformPosition.Left:
                         tilePosition = new Vector3Int(x, y - (yChoosed/2), 0);
                    break;
                    case PlatformPosition.Right:
                        tilePosition = new Vector3Int(- xChoosed + x, y - (yChoosed/2), 0);
                    break;
                    default:
                    case PlatformPosition.Center:
                        tilePosition = new Vector3Int(-(xChoosed/2) + x, y - (yChoosed/2), 0);
                    break;
                }
                
                tilemap.SetTile(cellPosition + tilePosition, tile);
            }
        }
    }


}