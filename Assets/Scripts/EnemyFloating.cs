using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyFloating: EnemyBase
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float maxDistanceVision = 10f;

    Path path;
    int currentWaypoint = 0;
    bool reachEndOfPath = false;

    Rigidbody2D rb2d;
    Seeker seeker;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
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
    void Start()
    {
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        {
            seeker.StartPath(rb2d.position, target.position, OnPathComplete);
        
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(path == null)
        {
            return;
        }

        if(Vector2.Distance(rb2d.position, target.position) > maxDistanceVision) {
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachEndOfPath = true;
            return;
        } else
        {
            reachEndOfPath = false;
        }

        Vector2 direction = ((Vector2) path.vectorPath[currentWaypoint] - rb2d.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        float distance = Vector2.Distance(rb2d.position, path.vectorPath[currentWaypoint]);

        rb2d.AddForce(force);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

}
