using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThemeToolCamera : MonoBehaviour {

    public ColorTypeTheme colorType;
    
    void Start()
    {
        ThemeManager.instance.SetColor(GetComponent<Camera>(), colorType);
    }
}