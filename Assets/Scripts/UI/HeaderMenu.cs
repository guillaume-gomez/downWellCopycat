using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeaderMenu : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI money;

    void Start()
    {
      MarketManager.instance.OnMoneyChange += OnMoneyChange;
      score.text = "Score: " + MarketManager.instance.Score.ToString("F2");
      money.text = "Money: " + MarketManager.instance.Money.ToString("F2");
    }

   private void OnMoneyChange(object sender, OnMoneyChangedEventArgs e)
   {
      money.text = "Money: " + e.money.ToString("F2");
   }

}
