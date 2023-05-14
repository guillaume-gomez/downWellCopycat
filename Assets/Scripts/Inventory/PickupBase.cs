using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBase : MonoBehaviour
{
    protected Inventory inventory;
    public string itemName = "not defined yet :-|";

    protected void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.transform.Find("WeaponPosition").GetComponent<Inventory>();
    }

    protected void NotifyPickedUp()
    {
        if(LevelManager.instance != null)
        {
            LevelManager.instance.PickedUp(itemName);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            inventory.AddItem(gameObject);
            NotifyPickedUp();
        }
    }
}
