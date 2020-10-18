using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBoughtItem : PickupBase
{
    public float price = 0.0f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            inventory.BuyItem(gameObject, price);
        }
    }
}
