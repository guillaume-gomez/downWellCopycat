using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThemeToolSprite : MonoBehaviour {

    public ColorTypeTheme colorType;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ThemeManager.instance.SetColor(spriteRenderer, colorType);
    }
}