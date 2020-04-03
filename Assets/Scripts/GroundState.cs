using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GroundState
{
    private GameObject player;
    private float  width;
    private float height;
    private float length;

    //GroundState constructor.  Sets offsets for raycasting.
    public GroundState(GameObject playerRef)
    {
        player = playerRef;
        width = player.GetComponent<BoxCollider2D>().bounds.extents.x + 0.1f;
        height = player.GetComponent<BoxCollider2D>().bounds.extents.y + 0.2f;
        length = 0.05f;
    }

    //Returns whether or not player is touching wall.
    public bool isWall()
    {
        bool left = Physics2D.Raycast(new Vector2(player.transform.position.x - width, player.transform.position.y), -Vector2.right, length);
        bool right = Physics2D.Raycast(new Vector2(player.transform.position.x + width, player.transform.position.y), Vector2.right, length);

        if(left || right)
            return true;
        else
            return false;
    }

    //Returns whether or not player is touching ground.
    public bool isGround()
    {
        bool bottom1 = Physics2D.Raycast(new Vector2(player.transform.position.x, player.transform.position.y - height), -Vector2.up, length);
        bool bottom2 = Physics2D.Raycast(new Vector2(player.transform.position.x + (width - 0.2f), player.transform.position.y - height), -Vector2.up, length);
        bool bottom3 = Physics2D.Raycast(new Vector2(player.transform.position.x - (width - 0.2f), player.transform.position.y - height), -Vector2.up, length);
        if(bottom1 || bottom2 || bottom3)
            return true;
        else
            return false;
    }

    //Returns whether or not player is touching wall or ground.
    public bool isTouching()
    {
        if(isGround() || isWall())
            return true;
        else
            return false;
    }

    //Returns direction of wall.
    public int wallDirection()
    {
        bool left = Physics2D.Raycast(new Vector2(player.transform.position.x - width, player.transform.position.y), -Vector2.right, length);
        bool right = Physics2D.Raycast(new Vector2(player.transform.position.x + width, player.transform.position.y), Vector2.right, length);

        if(left)
            return -1;
        else if(right)
            return 1;
        else
            return 0;
    }
}