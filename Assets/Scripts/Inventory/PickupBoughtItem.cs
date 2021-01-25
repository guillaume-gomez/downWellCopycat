using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBoughtItem : PickupBase
{
  public float price = 0.0f;
  private bool canBeBought;

  new void Start()
  {
    base.Start();
    LevelManager.instance.OnMoneyChange += OnMoneyChange;
    canBeBought = (GameManager.instance.LevelSystemRun.money <= price);
  }

  void OnMoneyChange(object sender, OnMoneyChangedEventArgs e)
  {
    canBeBought = (e.money <= price);
  }


  protected override void OnTriggerEnter2D(Collider2D other)
  {
    if(canBeBought)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            inventory.BuyItem(gameObject, price);
        }
    }
  }
}
