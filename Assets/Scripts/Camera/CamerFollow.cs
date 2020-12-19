using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CamerFollow : MonoBehaviour
{
    public LifeScript player;
    public float dampTime = 0.1f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    private bool shouldFollow = true;

    void Awake()
    {
        if(LevelManager.instance)
        {
            LevelManager.instance.OnWin += OnUnFollow;
        }
        player.OnLifeChanged += OnPlayerHurt;
    }

    void Update()
    {
        if(!shouldFollow)
        {
            return;
        }

        Vector3 cameraPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z) + offset;
        transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref velocity, dampTime);
    }

    public void Unfollow()
    {
        //Debug.Log("Unfollow");
        shouldFollow = false;
    }

    public void Follow()
    {
        //Debug.Log("Unfollow");
        shouldFollow = true;
    }

    private void OnUnFollow(object sender, System.EventArgs e)
    {
        Unfollow();
    }

    private void OnPlayerHurt(object sender, OnLifeChangedEventArgs e)
    {
        Debug.Log("OnPlayerHurt");
        if((int) e.life > 0 && (int) e.diff < 0)
        {
            StartCoroutine(Shake(1f, 0.2f));
        }
    }

    IEnumerator Shake(float duration, float magnitude)
    {
        float offset = 2.5f;
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-offset, offset) * magnitude;
            float y = Random.Range(-offset, offset) * magnitude;

            transform.localPosition = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = transform.position;
    }



}
