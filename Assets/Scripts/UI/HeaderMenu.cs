using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeaderMenu : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI money;
    public TextMeshProUGUI pricePaid;
    void Start()
    {
      MarketManager.instance.OnMoneyChange += OnMoneyChange;
      score.text = "Score: " + MarketManager.instance.Score.ToString("F2");
      money.text = "Money: " + MarketManager.instance.Money.ToString("F2");
      pricePaid.text = "";
    }

   private void OnMoneyChange(object sender, OnMoneyChangedEventArgs e)
   {
      money.text = "Money: " + e.money.ToString("F2");
      StartCoroutine(FlashPricePaid(e.pricePaid));
   }

   IEnumerator FlashPricePaid(float pricePaidEvent) {
      pricePaid.text = "-" + pricePaidEvent.ToString("F2");
      yield return new WaitForSeconds(1f);
      pricePaid.text = "";

   }

}
