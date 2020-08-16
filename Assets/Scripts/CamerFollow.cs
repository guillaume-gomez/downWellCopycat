using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CamerFollow : MonoBehaviour
{
    public PlayerController player;
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

    private void OnUnFollow(object sender, System.EventArgs e)
    {
        Unfollow();
    }

    void OnPlayerHurt(object sender, OnLifeChangedEventArgs e)
    {
        if((int) e.life > 0)
        {
            StartCoroutine(Shake(0.05f, 0.4f));
        }
    }

    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1.0f, 1.0f) * magnitude;
            float y = Random.Range(-1.0f, 1.0f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }


}
