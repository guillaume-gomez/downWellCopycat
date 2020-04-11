using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController player = collision.collider.GetComponent<PlayerController>();
            if(!player) {
                return;
            }
            player.Hurt();
        }
        if(collision.gameObject.tag == "Bullet")
        {
          Hurt();
        }
    }

    public void Hurt() {
      Destroy(this.gameObject);
    }
}