using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    private Transform weaponPosition;
    private Weapon activeWeapon; // slot 0 for the moment
    public event EventHandler<WeaponEventArgs> OnShootHandlerActiveWeapon;

    void Start()
    {
        weaponPosition = transform.Find("WeaponPosition");
        // if a weapon is already here
        if(slots.Length > 0)
        {
            SetWeapon(slots[0]);
        }
    }

    private void setWeapon(GameObject _weapon, bool checkFull)
    {
        // for the moment we have only one item
        for(int i = 0; i < slots.Length; ++i)
        {
            if(!checkFull || isFull[i] == false)
            {
                isFull[i] = true;
                slots[i] = _weapon;
                _weapon.transform.SetParent(weaponPosition);
                _weapon.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
                //remove handler from old activeWeapon
                if(activeWeapon)
                {
                    activeWeapon.OnShootHandler -= OnShootActiveWeapon;
                }
                activeWeapon = slots[i].GetComponent<Weapon>();
                //add handler' from the new activeWeapon
                activeWeapon.OnShootHandler += OnShootActiveWeapon;
                break;
            }
        }
    }

    public void SetWeapon(GameObject _weapon)
    {
        setWeapon(_weapon, true);
    }

    public void ReplaceWeapon(GameObject _weapon)
    {
        setWeapon(_weapon, false);
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

            return activeWeapon.Shoot();
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

    public int GetDamage()
    {
        if(activeWeapon)
        {
            return activeWeapon.GetDamage();
        }
        return 0;
    }

    protected virtual void OnShootActiveWeapon(object sender, WeaponEventArgs e)
    {
        WeaponEventArgs args = new WeaponEventArgs();
        args.bullet = e.bullet;

        if (OnShootHandlerActiveWeapon != null)
        {
            OnShootHandlerActiveWeapon(this, args);
        }
    }

    public void BuyItem(GameObject item, float price)
    {
        // To do add modifier, weapon, or anything else
        GameManager.instance.LevelSystemRun.money -= price;
        Destroy(item);
    }
}