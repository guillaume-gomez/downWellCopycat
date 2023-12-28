using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThemeToolCamera : MonoBehaviour {

    public ColorTypeTheme colorType;
    
    void Start()
    {
        SetColor(colorType);
        ThemeManager.instance.OnColorChanged += UpdateColor;
    }

    private void SetColor(ColorTypeTheme colorType) {
        ThemeManager.instance.SetColor(GetComponent<Camera>(), colorType);
    }

    private void UpdateColor(object sender, OnColorChangedEventArgs e) {
        Debug.Log("mad");
        SetColor(e.colorTypeTheme);
    }
}