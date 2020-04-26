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
    public bool hasLimitY = false;

    Path path;
    int currentWaypoint = 0;

    Rigidbody2D rb2d;
    Seeker seeker;

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
        seeker = GetComponent<Seeker>();
        // in case someone forgot to assign it
        if(!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
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
