using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMovingPlatform : MonoBehaviour {

    private bool isOnMovingPlatform;
    private float offsetToKeepPlayerAbovePlatform = 2.2f;
    private float min = 0.2f;
    private float max = 1.2f;
    protected Rigidbody2D rb2d;
    private int layerMastk;

    protected void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        // check raycast to everything except" enemy layer (espacially itself)
        layerMastk = 1 << gameObject.layer;
        layerMastk = ~layerMastk;
    }

    private void Update()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, -transform.up, 2f, layerMastk);
        Debug.DrawLine(transform.position, transform.position - (transform.up * 2f), Color.green, 2);
        if(groundInfo.collider != null)
        {
            Debug.Log(groundInfo.collider.gameObject.name);
            if(groundInfo.collider.gameObject.name.Contains("Cloud23"))
            {
                Transform movingPlatform  = groundInfo.collider.transform;

                if(movingPlatform.position.y - transform.position.y <= min && movingPlatform.position.y - transform.position.y >= max)
                {
                    isOnMovingPlatform = true;
                }
                else
                {
                    isOnMovingPlatform = false;
                }
            }

            if(isOnMovingPlatform)
            {

                rb2d.position = new Vector3(rb2d.position.x + 0.5f, groundInfo.transform.position.y + offsetToKeepPlayerAbovePlatform, 0);
            }
        }
    }

}