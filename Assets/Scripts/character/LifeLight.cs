using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LifeLight : MonoBehaviour
{

    public TimerDeath timerDeathScript;
    private Light2D light;
    private float initialPointLightOuterRadius;
    private float initialPointLightInnerRadius;

    void Start() {
        light = GetComponent<Light2D>();
        initialPointLightOuterRadius = light.pointLightOuterRadius;
        initialPointLightInnerRadius = light.pointLightInnerRadius;
    }

    void FixedUpdate()
    {
        if(LevelManager.PauseGame) {
            return;
        }
        light.pointLightOuterRadius = timerDeathScript.TimerDeathRatio * initialPointLightOuterRadius;
        light.pointLightInnerRadius = timerDeathScript.TimerDeathRatio * initialPointLightInnerRadius;
    }


}
