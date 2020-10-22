using UnityEngine;
using System.Collections;

public class BreakableBloc : MonoBehaviour
{
    public AudioClip explosionSound1;
    public AudioClip explosionSound2;
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
        spriteRenderer.sprite = dmgSprite;

        hp = hp - loss;
        if(hp <= 0)
        {
            Destroy(gameObject);
            SoundManager.instance.RandomizeSfx (explosionSound1, explosionSound2);
            Instantiate(explosionParticle, transform.position, transform.rotation);
            //gameObject.SetActive(false);
        }
    }
}