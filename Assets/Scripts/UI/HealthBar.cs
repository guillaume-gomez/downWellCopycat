using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    //public Gradient gradient;
    public TextMeshProUGUI text;
    public Image fill;
    public LifeScript player;

    private void Start()
    {
        player.OnLifeChanged += OnPlayerHurt;
        SetMaxHealth(player.Life);
        SetHealth(player.Life);
    }

    private void OnPlayerHurt(object sender, OnLifeChangedEventArgs e)
    {
        // later the params will give the value
        SetHealth((int) e.life);
        text.text = e.life.ToString();
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        text.text = health.ToString();

        //fill.color = gradient.Evaluate(1.0f);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        //fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
