using System;
using UnityEngine;

public class OnTimerDeathChangedEventArgs: EventArgs {
    public float elapsedTimerDeath { get; set; }
}

public class TimerDeath : MonoBehaviour
{
    public event EventHandler<OnTimerDeathChangedEventArgs> OnTimerDeathChanged;

    private LifeScript lifeScript;
    private float timerDeathValue = 10.0f;
    protected float elapsedTimerDeath = 0.0f;
    public bool godMode = false;

    public float TimerDeathValue {
        get => timerDeathValue;
    }

    public float TimerDeathRatio {
        get => (timerDeathValue - elapsedTimerDeath) / timerDeathValue;
    }

    void Start() {
        lifeScript = GetComponent<LifeScript>();
        lifeScript.OnLifeChanged += OnPlayerHurt;
    }

    void FixedUpdate()
    {
        if(LevelManager.PauseGame || godMode) {
            return;
        }

        elapsedTimerDeath = elapsedTimerDeath + Time.deltaTime;
        OnTimerDeathChangedEventArgs args = new OnTimerDeathChangedEventArgs();
        args.elapsedTimerDeath = timerDeathValue - elapsedTimerDeath;
        if(OnTimerDeathChanged != null)
        {
            OnTimerDeathChanged(this, args);
        }

        if(elapsedTimerDeath >= timerDeathValue) {
            lifeScript.LoseLife(Math.Max(lifeScript.Life - 1, 0));
            ResetTimer();
        }
    }

    public void ResetTimer() {
        elapsedTimerDeath = 0.0f;
    }

    private void OnPlayerHurt(object sender, OnLifeChangedEventArgs e)
    {
        //Debug.Log("OnPlayerHurt " +  e.life + " " +  e.diff);
        if((int) e.life > 0 && (int) e.diff < 0)
        {
            ResetTimer();
        }
    }



}
