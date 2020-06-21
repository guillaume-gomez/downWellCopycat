using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int coinValue = 10;

    public int CoinValue {
        get => coinValue;
        set => coinValue = value;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            LevelManager.instance.TakeMoney(coinValue);
            Destroy(gameObject);
        }
    }

}
