using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyFloating: EnemyBase
{

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float maxDistanceVision = 10f;
    public bool hasLimitY = false;
    public float rememberTime = 0.5f;

    private float remember;

    Path path;
    int currentWaypoint = 0;

    Rigidbody2D rb2d;
    Seeker seeker;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        remember = 0.0f;
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if(CannotMove())
        {
            return;
        }
        if(Vector2.Distance(rb2d.position, target.position) > maxDistanceVision)
        {
            return;
        }
        if(hasLimitY && rb2d.position.y <= target.position.y)
        {
            return;
        }

        if(seeker.IsDone())
        {
            seeker.StartPath(rb2d.position, target.position, OnPathComplete);
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        if(CannotMove())
        {
            return;
        }

        if(path == null)
        {
            return;
        }
        if(Vector2.Distance(rb2d.position, target.position) > maxDistanceVision)
        {
            return;
        }
        if(hasLimitY && rb2d.position.y <= target.position.y)
        {
            return;
        }
        if(currentWaypoint >= path.vectorPath.Count) //react end of path
        {
            return;
        }
        remember = remember + Time.deltaTime;

        float distance = Vector2.Distance(rb2d.position, path.vectorPath[currentWaypoint]);

        Vector2 direction = ((Vector2) path.vectorPath[currentWaypoint] - rb2d.position).normalized;
        Vector2 force = direction * speed; //* Time.deltaTime;
        if(remember >= rememberTime)
        {
            rb2d.AddForce(force);
            remember = 0.0f;
        }

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

}
