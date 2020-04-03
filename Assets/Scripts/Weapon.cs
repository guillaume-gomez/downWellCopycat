using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Rigidbody2D bullet;
    public float thrustBulletToPlayer = 10.0f;
    public float reloadTimerRemember = 0.3f;

    private float bulletSpeed = 10.0f;
    private float reloadTimer = 0.0f;

    public float TriggerGun()
    {
        if (reloadTimer >= reloadTimerRemember)
        {
            reloadTimer = 0.0f;
            Rigidbody2D bulletClone =  Instantiate(bullet, transform.position, transform.rotation);
            bulletClone.velocity = transform.TransformDirection(- transform.up * bulletSpeed);
            return thrustBulletToPlayer;
        }
        return 0.0f;
    }

    void FixedUpdate()
    {
        reloadTimer = reloadTimer + Time.deltaTime;
    }
}
