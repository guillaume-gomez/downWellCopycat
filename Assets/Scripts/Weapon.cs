using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Rigidbody2D bullet;
    public float thrustBulletToPlayer = 10.0f;
    public float shootPressedTimerRemember = 0.3f;

    private float bulletSpeed = 25.0f;
    private float shootPressedTimer = 0.0f;

    public int nbBullet = 12;
    protected int currentBullet = 12;

    public float Shoot()
    {
       // if (CanShoot())
        //{
            shootPressedTimer = 0.0f;
            Rigidbody2D bulletClone =  Instantiate(bullet, transform.position, transform.rotation);
            bulletClone.velocity = transform.TransformDirection(- transform.up * bulletSpeed);
            currentBullet = currentBullet - 1;
            return thrustBulletToPlayer;
        //}
        //return 0.0f;
    }

    public void StopShoot()
    {
        
    }

    public void Reload() 
    {
        currentBullet = nbBullet;
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
