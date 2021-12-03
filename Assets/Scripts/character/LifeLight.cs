using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LifeLight : MonoBehaviour
{

    public TimerDeath timerDeathScript;
    private Light2D light;

    void Start() {
        light = GetComponent<Light2D>();
        //timerDeathScript = GetComponent<TimerDeath>();
    }

    void FixedUpdate()
    {
        if(LevelManager.PauseGame) {
            return;
        }
        //Debug.Log(timerDeathScript.TimerDeathValue/ 10000 * light.pointLightInnerAngle);
        light.pointLightInnerAngle = timerDeathScript.TimerDeathValue/ 10000 * light.pointLightInnerAngle;

    }


}
