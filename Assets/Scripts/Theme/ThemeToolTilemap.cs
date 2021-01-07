using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public class ThemeToolTilemap : MonoBehaviour {

    public ColorTypeTheme colorType;
    private Tilemap tilemap;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        ThemeManager.instance.SetColor(tilemap, colorType);
    }
}