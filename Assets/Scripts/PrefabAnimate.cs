using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabAnimate : MonoBehaviour
{
    public AudioClip sound;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
           SoundManager.instance.PlaySingle(sound);
        }
    }
}
