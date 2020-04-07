using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPhysicsObject : PhysicsObject
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachEndOfPath = false;

    Seeker seeker;

    protected void OnEnable()
    {
        base.OnEnable();
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
    protected void Start()
    {
        base.Start();
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
    protected void Update()
    {
        base.Update();

        if(path == null)
        {
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
        targetVelocity = direction;

        float distance = Vector2.Distance(rb2d.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }

    public void Hurt()
    {
        Destroy(this.gameObject);
    }
}
