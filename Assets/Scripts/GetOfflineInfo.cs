using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GetOfflineInfo : MonoBehaviour
{
    public float distance;
    public float speed;
    public float offlinePenalty = 0.50f;
    public float offlineEarnings = 0f;
    public bool reachedEndOfPath = false;
    public Transform miningTarget;
    public Transform dropOffTarget;
    public float timeToMine = 10f;
    public float miningEarnings = 2f;
    public float miningEarningsPerSecond;
    public float roundTripTravelTime;

    public Path path;
    public float pathDistance;

    bool reportRun = false;

    void Start()
    {
        speed = gameObject.GetComponent<AIPath>().maxSpeed;
        miningTarget = gameObject.GetComponent<AIDestinationSetter>().target;
        path = gameObject.GetComponent<AIPath>().ThePath;
    }

    void Update()
    {
        reachedEndOfPath = gameObject.GetComponent<AIPath>().reachedEndOfPath;



        if (reachedEndOfPath)
        {
            distance = Vector2.Distance(dropOffTarget.position, miningTarget.position);
            miningEarningsPerSecond = (miningEarnings / timeToMine);
            roundTripTravelTime = ((distance * 2) / speed);

            offlineEarnings = (miningEarningsPerSecond / roundTripTravelTime * offlinePenalty);

            pathDistance = path.GetTotalLength();

            Report();
        }
    }

    void Report()
    {
        if (!reportRun)
        {
            Debug.Log("//////////////////////////////////////");
            Debug.Log("Path Distance = " + pathDistance);
            Debug.Log("Round Trip Distance Travelled = " + distance * 2);
            Debug.Log("Round Trip Travel Time = " + roundTripTravelTime);
            Debug.Log("Offline Earnings Per Second: $" + offlineEarnings.ToString("F6"));
            Debug.Log("//////////////////////////////////////");
            reportRun = true;
        }
    }
}
