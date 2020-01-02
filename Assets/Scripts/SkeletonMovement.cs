using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SkeletonMovement : MonoBehaviour
{
    private RaycastHit2D hitUp;
    private RaycastHit2D hitDown;
    private RaycastHit2D hitLeft;
    private RaycastHit2D hitRight;

    public LayerMask collideLayer;

    [SerializeField] Transform player;
    [SerializeField] Transform blockAbove;
    [SerializeField] Transform blockLeft;
    [SerializeField] Transform blockDown;
    [SerializeField] Transform blockRight;

    private Vector2 closestBlock;
    private Vector3 moveToPos;

    private string whichBlock;

    private bool moving;

    public float nextWayPointDistance;
    [SerializeField] float attackRange;

    Path path;  // current path we're following
    int currentWayPoint = 0;    // current way point along path
    bool reachedEndOfPath = false;  // did we reach end of path

    Seeker seeker;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        GetSurroundings();
    }

    void Update()
    {
        seeker.StartPath(transform.position, closestBlock, OnPathComplete);
        ReceieveInput();
        Movement();
    }

    private void GetSurroundings()
    {
        hitUp = Physics2D.Raycast(transform.position, Vector2.up, 1f, collideLayer.value);
        hitLeft = Physics2D.Raycast(transform.position, -Vector2.right, 1f, collideLayer.value);
        hitDown = Physics2D.Raycast(transform.position, -Vector2.up, 1f, collideLayer.value);
        hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1f, collideLayer.value);
    }

    private void ReceieveInput()
    {
        if (!moving)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                if (Vector2.Distance(transform.position, player.position) > attackRange) // if the player isn't within range for attack movement, move around randomly
                {
                    int rand = Random.Range(0, 4);
                    if (rand == 0)    // rolled to move up
                    {
                        if (!hitUp)
                        {
                            moving = true;
                            moveToPos = new Vector2(transform.position.x, transform.position.y + 1);
                        }
                    }
                    else if (rand == 1)   // rolled to move left
                    {
                        if (!hitLeft)
                        {
                            moving = true;
                            moveToPos = new Vector2(transform.position.x - 1, transform.position.y);
                        }
                    }
                    else if (rand == 2)   // rolled to move down
                    {
                        if (!hitDown)
                        {
                            moving = true;
                            moveToPos = new Vector2(transform.position.x, transform.position.y - 1);
                        }
                    }
                    else if (rand == 3)  // rolled to move right
                    {
                        if (!hitRight)
                        {
                            moving = true;
                            moveToPos = new Vector2(transform.position.x + 1, transform.position.y);
                        }
                    }
                }



                else     // player is within range, move in to attack
                {
                    // get distances to blocks around player
                    float distanceAbove = Vector2.Distance(transform.position, blockAbove.transform.position);
                    float distanceLeft = Vector2.Distance(transform.position, blockLeft.transform.position);
                    float distanceDown = Vector2.Distance(transform.position, blockDown.transform.position);
                    float distanceRight = Vector2.Distance(transform.position, blockRight.transform.position);

                    // find closest block to move towards
                    if (distanceAbove < distanceLeft && distanceAbove < distanceDown && distanceAbove < distanceRight)
                    {
                        closestBlock = blockAbove.transform.position;
                        whichBlock = "blockAbove";
                    }
                    if (distanceLeft < distanceAbove && distanceLeft < distanceDown && distanceLeft < distanceRight)
                    {
                        closestBlock = blockLeft.transform.position;
                        whichBlock = "blockLeft";
                    }
                    if (distanceDown < distanceAbove && distanceDown < distanceLeft && distanceDown < distanceRight)
                    {
                        closestBlock = blockDown.transform.position;
                        whichBlock = "blockDown";
                    }
                    if (distanceRight < distanceAbove && distanceRight < distanceLeft && distanceRight < distanceDown)
                    {
                        closestBlock = blockRight.transform.position;
                        whichBlock = "blockRight";
                    }
                    //Debug.Log("Closest player block is: " + whichBlock + " @ " + closestBlock);

                    //**************************//
                    //   BEGIN AI PATHFINDING   //
                    //**************************//

                    

                    if (path == null)
                    {
                        Debug.Log("Null path");
                        return;
                    }
                    else
                    {
                        moveToPos = path.vectorPath[1];
                        Debug.Log("AI moving to position: " + moveToPos);
                        moving = true;
                    }


                    //if (currentWayPoint >= path.vectorPath.Count)
                    //{
                    //    reachedEndOfPath = true;
                    //    return;
                    //}
                    //else
                    //{
                    //    reachedEndOfPath = false;
                    //    moveToPos = path.vectorPath[1];
                    //    Debug.Log("AI moving to position: " + moveToPos);
                    //    moving = true;
                    //}
                }
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    private void Movement()
    {
        if (moving)
        {
            if (transform.position != moveToPos)
            {
                transform.position = Vector2.MoveTowards(transform.position, moveToPos, Time.deltaTime * 10f);
            }
            else
            {
                moving = false;
                moveToPos = transform.position;
                GetSurroundings();
            }
        }
    }
}

