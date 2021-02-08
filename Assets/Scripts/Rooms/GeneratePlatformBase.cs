using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class GeneratePlatformBase : MonoBehaviour
{
    protected int width;
    protected int height;
    [Space]
    [Header("X values")]
    [Range(2, 24)]
    protected int xRangeMin;
    [Range(2, 24)]
    protected int xRangeMax;
    [Space]
    [Header("Y values")]
    [Range(1, 18)]
    protected int yRangeMin;
    [Range(1, 18)]
    protected int yRangeMax;
    [Space]
    protected PlatformPosition platformPosition;
    protected SpawnEnemy spawnEnemy;

    protected void setSize() {
        width = Random.Range(xRangeMin, xRangeMax);
        height = Random.Range(yRangeMin, yRangeMax);
    }

}