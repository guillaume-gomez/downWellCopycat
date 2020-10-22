using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponEventArgs : EventArgs
{
    public int bullet { get; set; }
}

public class Weapon : MonoBehaviour
{
    public string weaponName ="weapon";
    public Bullet bullet;
    public float thrustBulletToPlayer = 10.0f;
    public float shootPressedTimerRemember = 0.3f;
    public int damage = 1;
    public AudioClip shotSound;

    public event EventHandler<WeaponEventArgs> OnShootHandler;

    protected float shootPressedTimer = 0.0f;

    private int nbBullet = 12;
    protected int currentBullet = 12;

    public void Start()
    {
        if(GameManager.instance)
        {
            nbBullet = (int) GameManager.instance.CharacterStats.weaponAbilities.Value;
        }
        OnShoot(nbBullet);
    }

    public virtual float Shoot()
    {
        PlaySound();
        if(weaponName == "shotgun")
        {
            return ShotGunShoot();
        } else {
            return RegularShoot();
        }
    }

    private float RegularShoot()
    {
        shootPressedTimer = 0.0f;
        Bullet bulletObj =  Instantiate(bullet, transform.position, transform.rotation) as Bullet;
        bulletObj.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(- transform.up * bulletObj.speed);
        bulletObj.damage = damage;
        bulletObj.transform.parent = transform;

        currentBullet = currentBullet - 1;
        OnShoot(currentBullet);

        return thrustBulletToPlayer;
    }

    private float ShotGunShoot()
    {
        shootPressedTimer = 0.0f;
        Bullet bulletObj1 = Instantiate(bullet, transform.position, transform.rotation) as Bullet;
        bulletObj1.transform.Rotate(0.0f, 0.0f, -45.0f, Space.Self);
        bulletObj1.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(- new Vector3(1.0f, 1.0f, 0.0f) * bulletObj1.speed);
        bulletObj1.damage = damage;
        bulletObj1.transform.parent = transform;

        Bullet bulletObj2 = Instantiate(bullet, transform.position, transform.rotation) as Bullet;
        bulletObj2.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(- transform.up * bulletObj2.speed);
        bulletObj2.damage = damage;
        bulletObj2.transform.parent = transform;

        Bullet bulletObj3 = Instantiate(bullet, transform.position, transform.rotation) as Bullet;
        bulletObj3.transform.Rotate(0.0f, 0.0f, 45.0f, Space.Self);
        bulletObj3.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(- new Vector3(-1.0f, 1.0f, 0.0f) * bulletObj3.speed);
        bulletObj3.damage = damage;
        bulletObj3.transform.parent = transform;

        currentBullet = currentBullet - 1;
        OnShoot(currentBullet);

        return thrustBulletToPlayer;
    }

    public void Reload()
    {
        currentBullet = nbBullet;
        OnShoot(currentBullet);
    }

    public bool CanShoot()
    {
        return shootPressedTimer >= shootPressedTimerRemember && currentBullet > 0;
    }

    public int GetDamage()
    {
        return damage;
    }

    private void PlaySound()
    {
        if(SoundManager.instance)
        {
            SoundManager.instance.PlaySingle(shotSound);
        }
    }

    void FixedUpdate()
    {
        shootPressedTimer = shootPressedTimer + Time.deltaTime;
    }

    protected virtual void OnShoot(int bullet)
    {
        WeaponEventArgs args = new WeaponEventArgs();
        args.bullet = bullet;

        if (OnShootHandler != null)
        {
            OnShootHandler(this, args);
        }
    }
}
