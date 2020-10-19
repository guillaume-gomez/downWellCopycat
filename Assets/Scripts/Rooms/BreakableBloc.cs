using UnityEngine;
using System.Collections;

public class BreakableBloc : MonoBehaviour
{
    public AudioClip chopSound1;
    public AudioClip chopSound2;
    public GameObject explosionParticle;
    public Sprite dmgSprite;
    public float hp = 3.0f;


    private SpriteRenderer spriteRenderer;


    void Awake ()
    {
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }


    public void DamageBloc(float loss)
    {
        //SoundManager.instance.RandomizeSfx (chopSound1, chopSound2);
        spriteRenderer.sprite = dmgSprite;

        hp = hp - loss;
        if(hp <= 0)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, transform.rotation);
            //gameObject.SetActive(false);
        }
    }
}