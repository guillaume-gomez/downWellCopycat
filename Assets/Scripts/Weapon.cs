using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Rigidbody2D bullet;
    public float thrustBulletToPlayer = 10.0f;
    public float shootPressedTimerRemember = 0.3f;

    private float bulletSpeed = 25.0f;
    private float shootPressedTimer = 0.0f;

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

    public float Shoot()
    {
        shootPressedTimer = 0.0f;
        Rigidbody2D bulletClone =  Instantiate(bullet, transform.position, transform.rotation);
        bulletClone.velocity = transform.TransformDirection(- transform.up * bulletSpeed);
        currentBullet = currentBullet - 1;
        if(slider)
        {
            slider.value = currentBullet;
        }
        return thrustBulletToPlayer;
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

    void FixedUpdate()
    {
        shootPressedTimer = shootPressedTimer + Time.deltaTime;
    }
}
