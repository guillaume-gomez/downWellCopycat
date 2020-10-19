using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBoughtItem : PickupBase
{
  public float price = 0.0f;
  private bool canBeBought;

  void Start()
  {
    base.Start();
    LevelManager.instance.OnMoneyChange += OnMoneyChange;
  }

  void OnMoneyChange(object sender, OnMoneyChangedEventArgs e)
  {
    canBeBought = (e.money <= price);
  }


  void OnTriggerEnter2D(Collider2D other)
  {
    if(canBeBought)
    {
        if(other.CompareTag("Player"))
        {
            inventory.BuyItem(gameObject, price);
        }
    }
  }
}
