using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float coinValue = 10;
    private float alpha = 1.0f;
    private float angle = 0.0f;
    private SpriteRenderer sprite;
    public AudioClip pickUpSound;

    public float CoinValue {
        get => coinValue;
        set => coinValue = value;
    }

    void Start()
    {
         sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // transform.Rotate(Vector3.forward * angle);
        angle += 0.5f * Time.deltaTime;

        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
        alpha -= 0.25f * Time.deltaTime;

        if(alpha <= 0.0f) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(LevelManager.instance)
            {
                LevelManager.instance.UpdateMoney(coinValue);
            }
            if(SoundManager.instance)
            {
                SoundManager.instance.PlaySingle(pickUpSound);
            }
            Destroy(gameObject);
        }
    }

}
