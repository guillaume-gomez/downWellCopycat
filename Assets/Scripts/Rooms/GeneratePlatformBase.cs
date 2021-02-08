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
    public int xRangeMin;
    [Range(2, 24)]
    public int xRangeMax;
    [Space]
    [Header("Y values")]
    [Range(1, 18)]
    public int yRangeMin;
    [Range(1, 18)]
    public int yRangeMax;
    [Space]
    public PlatformPosition platformPosition;

    protected void SetSize() {
        width = Random.Range(xRangeMin, xRangeMax);
        height = Random.Range(yRangeMin, yRangeMax);
        Debug.Log(width);
        Debug.Log(height);
    }

}