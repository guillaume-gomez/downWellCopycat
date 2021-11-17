using System;
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
NoMoneyBloc,
BackgroundGui,
BackgroundMainMenu,
ButtonGui,
SliderBackgroundGui,
TextColor,
BonusItemGuiBorder,
BonusItemGuiBackground,
MarketBackground,
HealthBarFill,
WeaponBarFill,
EndLevelBackground,
ScoreLineColor,
}

public class OnColorChangedEventArgs : EventArgs
{
    public Color color { get; set; }
    public ColorTypeTheme colorTypeTheme { get; set;}
}

public class ThemeManager : MonoBehaviour {
    [Header("Game")]
    public Color background;
    public Color player;
    public Color enemy;
    public Color items;
    public Color blocs;
    [Space]
    [Header("Shop")]
    public Color noMoneyBloc;
    [Space]
    [Header("Gui")]
    public Color backgroundMainMenu;
    public Color backgroundGui;
    public Color buttonGui;
    public Color textColor;
    public Color sliderBackgroundGui;
    public Color bonusItemGuiBorder;
    public Color bonusItemGuiBackground;
    public Color marketBackground;
    public Color healthBarFill;
    public Color weaponBarFill;
    public Color endLevelBackground;
    public Color scoreLineColor;

    public static ThemeManager instance = null;
    public event EventHandler<OnColorChangedEventArgs> OnColorChanged;

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
            case ColorTypeTheme.NoMoneyBloc:
                return noMoneyBloc;
            case ColorTypeTheme.BackgroundGui:
                return backgroundGui;
            case ColorTypeTheme.BackgroundMainMenu:
                return backgroundMainMenu;
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
            case ColorTypeTheme.HealthBarFill:
                return healthBarFill;
            case ColorTypeTheme.WeaponBarFill:
                return weaponBarFill;
            case ColorTypeTheme.EndLevelBackground:
                return endLevelBackground;
            case ColorTypeTheme.ScoreLineColor:
                return scoreLineColor;
            default:
                return background;
        }
    }

    private ColorTypeTheme FieldnameToColorType(string fieldname)
    {
        switch(fieldname)
        {
            case "background":
                return ColorTypeTheme.Background;
            case "player":
                return ColorTypeTheme.Player;
            case "enemy":
                return ColorTypeTheme.Enemy;
            case "items":
                return ColorTypeTheme.Items;
            case "blocs":
                return ColorTypeTheme.Blocs;
            case "noMoneyBloc":
                return ColorTypeTheme.NoMoneyBloc;
            case "backgroundGui":
                return ColorTypeTheme.BackgroundGui;
            case "buttonGui":
                return ColorTypeTheme.ButtonGui;
            case "textColor":
                return ColorTypeTheme.TextColor;
            case "backgroundMainMenu":
                return ColorTypeTheme.BackgroundMainMenu;
            case "sliderBackgroundGui":
                return ColorTypeTheme.SliderBackgroundGui;
            case "bonusItemGuiBorder":
                return ColorTypeTheme.BonusItemGuiBorder;
            case "bonusItemGuiBackground":
                return ColorTypeTheme.BonusItemGuiBackground;
            case "marketBackground":
                return ColorTypeTheme.MarketBackground;
            case "healthBarFill":
                return ColorTypeTheme.HealthBarFill;
            case "weaponBarFill":
                return ColorTypeTheme.WeaponBarFill;
            case "endLevelBackground":
                return ColorTypeTheme.EndLevelBackground;
            case "scoreLineColor":
                return ColorTypeTheme.ScoreLineColor;
            default:
                return ColorTypeTheme.Background;
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

    public void UpdatePalette(Color color, string fieldname) {
        // generic setter
        var field = GetType().GetField(fieldname);
        field.SetValue(this, color);

        Debug.Log(background);

        OnColorChangedEventArgs args = new OnColorChangedEventArgs();
        args.color = color;
        args.colorTypeTheme = FieldnameToColorType(fieldname);
        if(OnColorChanged != null)
        {
            OnColorChanged(this, args);
        }
    }
}