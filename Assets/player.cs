using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class player : MonoBehaviour
{
    private float xInput, yInput;

    [SerializeField] private float movespeed, jumpspeed;
    [SerializeField] private Rigidbody2D rb;

    [Header("Collision info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;

    private Animator anim;
    private int facingDirection = 1;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        movespeed = 5;
        jumpspeed = 10;
        anim = GetComponentInChildren<Animator>();
        //isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        CheckInput();
        CollisionCheck();
        FlipController();
        AnimatorControllers();

    }

    private void CollisionCheck()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        //yInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Down();
        }
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xInput * movespeed, rb.velocity.y);
        //rb.velocity = new Vector2(rb.velocity.x, yInput * jumpspeed);
    }

    private void Down()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpspeed * (-1));
    }

    private void Jump()
    {
        if (isGrounded) rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
    }

    private void AnimatorControllers()
    {
        bool isMoving = rb.velocity.x != 0;
        anim.SetFloat("yVelocity", rb.velocity.y);

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
    }
    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    private void FlipController()
    {
        if (rb.velocity.x > 0 && !facingRight)
            Flip();
        else if (rb.velocity.x < 0 && facingRight)
            Flip();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
