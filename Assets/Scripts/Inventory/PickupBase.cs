using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBase : MonoBehaviour
{
    protected Inventory inventory;

    protected void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.transform.Find("WeaponPosition").GetComponent<Inventory>();
    }
}
