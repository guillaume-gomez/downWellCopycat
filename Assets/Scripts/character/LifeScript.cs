using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class OnLifeChangedEventArgs : EventArgs
{
    public int life { get; set; }
    public int diff { get; set; }
}


public class LifeScript : MonoBehaviour
{
    public event EventHandler<OnLifeChangedEventArgs> OnLifeChanged;

    public SpriteRenderer spriteRenderer;
    public bool godMode;
    public int Life {
        get => life;
        set {
            life = value;
        }
    }
    public float unvisibleTimer = 1.0f;
    public bool Unvisible {
        get => unvisible;
    }
    private bool unvisible = false;
    private int life = 4;

    void Start()
    {
        if(GameManager.instance)
        {
            life = (int) GameManager.instance.LevelSystemRun.currentLife;
            // notify others by calling the setter
            Life = life;
        }
    }

    public void Hurt(EnemyBase enemy)
    {
        if(!unvisible && !godMode) {
            if(life >= 1) {
                // todo add armor
                LoseLife(Math.Max(life - enemy.Damage, 0));
                StartCoroutine(GetUnvisible(unvisibleTimer, enemy));
            }

            if(life <= 0)
            {
                LevelManager.instance.GameOver();
                return;
            }
            LevelManager.instance.UpdateLife(life);
        }
    }

    public void LoseLife(int newLife) {
        OnLifeChangedEventArgs args = new OnLifeChangedEventArgs();
        args.life = newLife;
        args.diff = newLife - life;
        if(OnLifeChanged != null)
        {
            OnLifeChanged(this, args);
        }
        Life = newLife;
        StartCoroutine(FlashSprite(spriteRenderer, 0.0f, 1.0f, 0.1f, unvisibleTimer));
        //Flash();
        // SoundManager.instance.PlaySingle(hurtSound);
    }

    private void Flash() {
        Debug.Log("fdfsd");
        spriteRenderer.DOColor(UnityEngine.Random.ColorHSV(), 0.7f).SetEase(Ease.InFlash, 4, 0);
    }

    IEnumerator FlashSprite(SpriteRenderer renderer, float minAlpha, float maxAlpha, float interval, float duration)
    {
        Color colorNow = renderer.color;
        Color minColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, minAlpha);
        Color maxColor = new Color(renderer.color.r, renderer.color.g, renderer.color.b, maxAlpha);
        float currentInterval = 0;
        while(duration > 0)
        {
            float tColor = currentInterval / interval;
            renderer.color = Color.Lerp(minColor, maxColor, tColor);

            currentInterval += Time.deltaTime;
            if(currentInterval >= interval)
            {
                Color temp = minColor;
                minColor = maxColor;
                maxColor = temp;
                currentInterval = currentInterval - interval;
            }
            duration -= Time.deltaTime;
            yield return null;
        }
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, maxAlpha);
    }

    IEnumerator GetUnvisible(float unvisibleTimer, EnemyBase enemy)
    {
        unvisible = true;
        if(enemy)
        {
            enemy.gameObject.layer = LayerMask.NameToLayer("enemy_hurt");
        }
        yield return new WaitForSeconds(unvisibleTimer);
        unvisible = false;
        if(enemy)
        {
            enemy.gameObject.layer = LayerMask.NameToLayer("enemy");
        }
    }

}
