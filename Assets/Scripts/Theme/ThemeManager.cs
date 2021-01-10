using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;

public enum ColorTypeTheme {
Background,
Player,
Enemy,
Items,
Blocs,
BackgroundGui,
ButtonGui,
SliderBackgroundGui,
TextColor,
BonusItemGuiBorder,
BonusItemGuiBackground,
MarketBackground,
}

public class ThemeManager : MonoBehaviour {
    [Header("Game")]
    public Color background;
    public Color player;
    public Color enemy;
    public Color items;
    public Color blocs;
    [Space]
    [Header("Gui")]
    public Color backgroundGui;
    public Color buttonGui;
    public Color textColor;
    public Color sliderBackgroundGui;
    public Color bonusItemGuiBorder;
    public Color bonusItemGuiBackground;
    public Color marketBackground;

    public static ThemeManager instance = null;

    void Awake()
    {
      if (instance == null)
      {
          instance = this;
      } else if (instance != this)
      {
          Destroy(gameObject);
      }
    }

    private Color ColorByName(ColorTypeTheme colorName)
    {
        switch(colorName)
        {
            case ColorTypeTheme.Background:
                return background;
            case ColorTypeTheme.Player:
                return player;
            case ColorTypeTheme.Enemy:
                return enemy;
            case ColorTypeTheme.Items:
                return items;
            case ColorTypeTheme.Blocs:
                return blocs;
            case ColorTypeTheme.BackgroundGui:
                return backgroundGui;
            case ColorTypeTheme.ButtonGui:
                return buttonGui;
            case ColorTypeTheme.TextColor:
                return textColor;
            case ColorTypeTheme.SliderBackgroundGui:
                return sliderBackgroundGui;
            case ColorTypeTheme.BonusItemGuiBorder:
                return bonusItemGuiBorder;
            case ColorTypeTheme.BonusItemGuiBackground:
                return bonusItemGuiBackground;
            case ColorTypeTheme.MarketBackground:
                return marketBackground;
            default:
                return background;
        }
    }

    public void SetColor(SpriteRenderer spriteRenderer, ColorTypeTheme typeColor)
    {
        spriteRenderer.color = ColorByName(typeColor);
    }

    public void SetColor(Tilemap tilemap, ColorTypeTheme typeColor)
    {
        tilemap.color = ColorByName(typeColor);
    }

    public void SetColor(Camera camera, ColorTypeTheme typeColor)
    {
        camera.backgroundColor = ColorByName(typeColor);
    }

    public void SetColor(Image image, ColorTypeTheme typeColor)
    {
        image.color = ColorByName(typeColor);
    }
}