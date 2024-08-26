using UnityEngine;

public class Player : MonoBehaviour
{
    private float xInput, yInput;

    [SerializeField] private float movespeed, jumpspeed;
    [SerializeField] private Rigidbody2D rb;

    [Header("Attack info")]
    private bool isAttacking;
    private int comboCounter;


    [Header("Dash info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCoolDown;
    [SerializeField] private float dashCoolDownTimer;

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
        dashDuration = 0.15F;
        dashSpeed = 30;
        dashCoolDown = 1;
        anim = GetComponentInChildren<Animator>();
        //isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {

        dashTime -= Time.deltaTime;
        dashCoolDownTimer -= Time.deltaTime;


        Movement();
        CheckInput();
        CollisionCheck();
        FlipController();
        AnimatorControllers();

    }

    public void AttackOver()
    {
        isAttacking = false;
    }
    
    private void CollisionCheck()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        //yInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.K))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Down();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            DashAbility();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            isAttacking = true;
        }
    }

    private void DashAbility()
    {
        if (dashCoolDownTimer < 0)
        {
            dashCoolDownTimer = dashCoolDown;
            dashTime = dashDuration;
        }
    }

    private void Movement()
    {
        if (dashTime > 0)
        {
            rb.velocity = new Vector2(xInput * dashSpeed, 0);
        }
        else
        {
            rb.velocity = new Vector2(xInput * movespeed, rb.velocity.y);
        }
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
        anim.SetBool("isDushing", dashTime > 0);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("comboCounter", comboCounter);
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
