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
        BreakableBloc bloc = collision.collider.GetComponent<BreakableBloc>();
        if(bloc) {
          bloc.DamageBloc(damage);
        }

        EnemyBase enemy = collision.collider.GetComponent<EnemyBase>();
        if(enemy) {
            enemy.Hurt(damage);
        }
        Destroy(gameObject);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
