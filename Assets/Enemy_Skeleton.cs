using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Entity
{
    [Header("Move info")]
    [SerializeField] private float moveSpeed;

    [Header("Player Detection")]
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;

    private RaycastHit2D isPlayerDetected;

    protected override void Start()
    {
        base.Start();
        moveSpeed = 1;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (isPlayerDetected)
        {
            if (isPlayerDetected.distance > 1)
            {
                rb.velocity = new Vector2(facingDirection * 3 * moveSpeed, rb.velocity.y);
                Debug.Log("I See");
            }
            else
            {
                Debug.Log("Attack" + isPlayerDetected);
                isAttacking = true;
            }
        }
        Movement();
        if (!isGrounded || isWallDetected)
        {
            Flip();
        }
    }

    private void Movement()
    {
        if (!isAttacking && !isPlayerDetected)
        {
            rb.velocity = new Vector2(facingDirection * moveSpeed, rb.velocity.y);    
        }

    }

    protected override void CollisionCheck()
    {
        base.CollisionCheck();
        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facingDirection, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * facingDirection, transform.position.y));
    }
}
