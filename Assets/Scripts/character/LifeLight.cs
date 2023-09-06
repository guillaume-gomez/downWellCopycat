using System;
using UnityEngine;


public class LifeLight : MonoBehaviour
{

    public TimerDeath timerDeathScript;
    private UnityEngine.Rendering.Universal.Light2D light;
    private float initialPointLightOuterRadius;
    private float initialPointLightInnerRadius;
    private Gradient gradient;

    void Start() {
        light = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        initialPointLightOuterRadius = light.pointLightOuterRadius;
        initialPointLightInnerRadius = light.pointLightInnerRadius;

        gradient = new Gradient();

        // Blend color from red at 0% to light.Color at 100%
        GradientColorKey[] colors = new GradientColorKey[2];
        colors[0] = new GradientColorKey(new Color(0.86f, 0.51f, 0.51f, 1.0f), 0.0f);
        colors[1] = new GradientColorKey(light.color, 1.0f);

        // keep the alpha at 100%
        GradientAlphaKey[] alphas = new GradientAlphaKey[2];
        alphas[0] = new GradientAlphaKey(1.0f, 1.0f);
        alphas[1] = new GradientAlphaKey(1.0f, 1.0f);

        gradient.SetKeys(colors, alphas);
    }

    void FixedUpdate()
    {
        if(LevelManager.PauseGame) {
            return;
        }
        light.pointLightOuterRadius = timerDeathScript.TimerDeathRatio * initialPointLightOuterRadius;
        light.pointLightInnerRadius = timerDeathScript.TimerDeathRatio * initialPointLightInnerRadius;
        light.color = gradient.Evaluate(timerDeathScript.TimerDeathRatio);
    }


}
