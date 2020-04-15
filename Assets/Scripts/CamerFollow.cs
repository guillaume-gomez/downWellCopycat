using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    public Transform player;
    public float dampTime = 0.1f;
    public float zDistance = -10f;
    private Vector3 cameraPos;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        cameraPos = new Vector3(player.position.x, player.position.y, zDistance);
        transform.position = Vector3.SmoothDamp(gameObject.transform.position, cameraPos, ref velocity, dampTime);
    }
}
