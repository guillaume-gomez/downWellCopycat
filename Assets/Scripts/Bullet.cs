using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {   
        transform.Rotate(new Vector3(0,0,90));
        // if body does not collide, it destroy itself after 2 secondes
        Destroy(gameObject, 2);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        BreakableBloc bloc = collision.collider.GetComponent<BreakableBloc>();
        if(bloc) {
            bloc.DamageBloc(1);
            return;
        }

        EnemyBase enemy = collision.collider.GetComponent<EnemyBase>();
        if(enemy) {
            enemy.Hurt();
            return;
        }
    }

}
