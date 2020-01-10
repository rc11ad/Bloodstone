using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MinerAI : MonoBehaviour
{

    public Transform target;

    public float speed = 200;
    public float nextWaypointDistance = 3f; // how close miner needs to be to a waypoint before it moves on to the next one

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();

        seeker.StartPath(transform.position, target.position, OnPathComplete);
    }

    // Update is called once per frame
    void Update()
    {
        if(path == null)
        {
            return;
        }


        // make sure we haven't reached the end of the path before moving
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
        }
        else
        {
            reachedEndOfPath = false;
        }

        transform.Translate(path.vectorPath[currentWaypoint]);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
