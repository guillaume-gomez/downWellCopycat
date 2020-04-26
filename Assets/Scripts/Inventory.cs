using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    private Transform weaponPosition;
    private Weapon activeWeapon; // slot 0 for the moment

    void Awake()
    {
        weaponPosition = transform.Find("WeaponPosition");
        Debug.Log(weaponPosition);
        // if a weapon is already here
        if(slots.Length > 0)
        {
            activeWeapon = slots[0].GetComponent<Weapon>();
            slots[0].transform.SetParent(weaponPosition);
            slots[0].transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        }
    }

    public void SetWeapon(GameObject _weapon)
    {
        // for the moment we have only one item
        for(int i = 0; i < slots.Length; ++i)
        {
            if(isFull[i] == false)
            {
                isFull[i] = true;
                slots[i] = _weapon;
                _weapon.transform.SetParent(weaponPosition);
                _weapon.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                activeWeapon = slots[i].GetComponent<Weapon>();
                break;
            }
        }
    }

    public Weapon ActiveWeapon()
    {
        return activeWeapon;
    }


    public bool CanShoot()
    {
        if(activeWeapon)
        {
            return activeWeapon.CanShoot();
        }
        return false;
    }

    public float Shoot()
    {
        if(activeWeapon)
        {
            activeWeapon.Shoot();
        }
        return 0.0f;
    }

    public void Reload()
    {
        if(activeWeapon)
        {
            activeWeapon.Reload();
        }
    }

    public float GetDamage()
    {
        if(activeWeapon)
        {
            return activeWeapon.GetDamage();
        }
        return 0;
    }

}