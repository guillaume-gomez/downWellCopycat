using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ThemeToolImage : MonoBehaviour {

    public ColorTypeTheme colorType;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        ThemeManager.instance.SetColor(image, colorType);
    }
}