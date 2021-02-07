using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : PickupBase
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            inventory.ReplaceWeapon(gameObject);
            NotifyPickedUp();
        }
    }
}
