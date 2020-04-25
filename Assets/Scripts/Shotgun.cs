using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shotgun : Weapon
{

    public override float Shoot()
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
        base.UpdateUI(currentBullet);
        return thrustBulletToPlayer;
    }

}
