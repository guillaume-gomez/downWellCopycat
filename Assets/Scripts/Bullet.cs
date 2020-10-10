using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public int speed = 50;

    void Start()
    {
        transform.Rotate(new Vector3(0,0,90));
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
       Destroy(gameObject);
        BreakableBloc bloc = collision.collider.GetComponent<BreakableBloc>();
        if(bloc) {
            bloc.DamageBloc(damage);
            return;
        }

        EnemyBase enemy = collision.collider.GetComponent<EnemyBase>();
        if(enemy) {
            enemy.Hurt(damage);
            if(enemy.Life == 0)
            {
                //Notify the weapon that he can reload
                Weapon weaponScript = transform.parent.GetComponent<Weapon>();
                weaponScript.Reload();
            }
            return;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
