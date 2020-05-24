using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public string weaponName ="weapon";
    public GameObject bullet;
    public float thrustBulletToPlayer = 10.0f;
    public float shootPressedTimerRemember = 0.3f;
    public float damage = 1.0f;

    protected float bulletSpeed = 35.0f;
    protected float shootPressedTimer = 0.0f;

    public int nbBullet = 12;
    protected int currentBullet = 12;

    private Slider slider;

    public void Start()
    {
        GameObject sliderObj = GameObject.Find("WeaponBarSlider");
        if(sliderObj)
        {
            slider = sliderObj.GetComponent<Slider>();
            slider.maxValue = nbBullet;
            slider.value = nbBullet;
        }
    }

    public virtual float Shoot()
    {
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
        GameObject bulletObj =  Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        bulletObj.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(- transform.up * bulletSpeed);
        bulletObj.GetComponent<Bullet>().damage = damage;

        currentBullet = currentBullet - 1;
        UpdateUI(currentBullet);
        return thrustBulletToPlayer;
    }

    private float ShotGunShoot()
    {
        shootPressedTimer = 0.0f;
        GameObject bulletObj1 = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        bulletObj1.transform.Rotate(0.0f, 0.0f, -45.0f, Space.Self);
        bulletObj1.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(- new Vector3(1.0f, 1.0f, 0.0f) * bulletSpeed);
        bulletObj1.GetComponent<Bullet>().damage = damage;

        GameObject bulletObj2 = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        bulletObj2.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(- transform.up * bulletSpeed);
        bulletObj2.GetComponent<Bullet>().damage = damage;

        GameObject bulletObj3 = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        bulletObj3.transform.Rotate(0.0f, 0.0f, 45.0f, Space.Self);
        bulletObj3.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(- new Vector3(-1.0f, 1.0f, 0.0f) * bulletSpeed);
        bulletObj3.GetComponent<Bullet>().damage = damage;


        currentBullet = currentBullet - 1;
        UpdateUI(currentBullet);
        return thrustBulletToPlayer;
    }

    protected void UpdateUI(float _currentBullet)
    {
        if(slider)
        {
            slider.value = _currentBullet;
        }
    }

    public void Reload()
    {
        currentBullet = nbBullet;
        if(slider)
        {
            slider.value = currentBullet;
        }
    }

    public bool CanShoot()
    {
        return shootPressedTimer >= shootPressedTimerRemember && currentBullet > 0;
    }

    public float GetDamage()
    {
        return damage;
    }

    void FixedUpdate()
    {
        shootPressedTimer = shootPressedTimer + Time.deltaTime;
    }
}
