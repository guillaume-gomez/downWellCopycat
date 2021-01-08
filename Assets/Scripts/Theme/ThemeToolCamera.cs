using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThemeToolCamera : MonoBehaviour {

    public ColorTypeTheme colorType;
    private Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
        ThemeManager.instance.SetColor(camera, colorType);
    }
}