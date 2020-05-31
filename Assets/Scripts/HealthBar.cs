using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public PlayerController playerController;

    private void Awake()
    {
        playerController.OnHurt += OnPlayerHurt;
        SetMaxHealth(playerController.Life);
    }

    private void OnPlayerHurt(object sender, System.EventArgs e)
    {
        // later the params will give the value
        SetHealth((int) slider.value - 1);
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1.0f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
