using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerMovement : MonoBehaviour
{
    RaycastHit2D hitUp;
    RaycastHit2D hitDown;
    RaycastHit2D hitLeft;
    RaycastHit2D hitRight;

    public LayerMask collideLayer;

    private bool moving = false;

    private Vector3 moveToPos;

    void Start()
    {
       GetSurroundings();
    }
    
    void Update()
    {
        GetInput();
        Movement();
    }

    private void GetSurroundings()
    {
        hitUp = Physics2D.Raycast(transform.position, Vector2.up, 1f, collideLayer.value);
        hitLeft = Physics2D.Raycast(transform.position, -Vector2.right, 1f, collideLayer.value);
        hitDown = Physics2D.Raycast(transform.position, -Vector2.up, 1f, collideLayer.value);
        hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1f, collideLayer.value);
    }

    private void GetInput()
    {
        if (!moving)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (!hitUp)
                {
                    moving = true;
                    moveToPos = new Vector2(transform.position.x, transform.position.y + 1);
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (!hitLeft)
                {
                    moving = true;
                    moveToPos = new Vector2(transform.position.x - 1, transform.position.y);
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (!hitDown)
                {
                    moving = true;
                    moveToPos = new Vector2(transform.position.x, transform.position.y - 1);
                }
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (!hitRight)
                {
                    moving = true;
                    moveToPos = new Vector2(transform.position.x + 1, transform.position.y);
                }
            }
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
                GetSurroundings();
            }
        }
    }
}
