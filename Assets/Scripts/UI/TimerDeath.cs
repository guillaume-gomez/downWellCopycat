using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerDeath : MonoBehaviour
{
    private Slider slider;
    public LifeScript player;

    void Start()
    {
        slider = GetComponent<Slider>();
        player.OnTimerDeathChanged += OnTimerDeath;
        slider.maxValue = player.TimerDeathValue;
    }

    private void OnTimerDeath(object sender, OnTimerDeathChangedEventArgs e)
    {
        slider.value = e.elapsedTimerDeath;
        //text.text = e.elapsedTimerDeath.ToString();
    }
}