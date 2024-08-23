using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class player : MonoBehaviour
{
    private float xInput,yInput;
    [SerializeField] private float movespeed,jumpspeed;
    [SerializeField] private Rigidbody2D rb;
    private Animator anim;
    private int facingDirection = 1;
    private bool facingRight = true;
    private int jumpcount = 1;
    // Start is called before the first frame update
    void Start()
    {
        movespeed = 5;
        jumpspeed = 5;
        anim = GetComponentInChildren<Animator>();
        //isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
 
        Movement();
        ChectInput();
        FlipController();
        AnimatorControllers();

    }

    private void ChectInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        //yInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpController();
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
        rb.velocity = new Vector2(rb.velocity.x, jumpspeed);
    }

    private void JumpController()
    {
        if (jumpcount > 0)
        {
        Jump();
        jumpcount--;
        }
    }

    private void AnimatorControllers()
    {
        bool isMoving = rb.velocity.x != 0;     
        anim.SetBool("isMoving", isMoving);
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
}
