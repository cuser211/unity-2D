using UnityEngine;

public class Player : Entity
{
    [Header("Move info")]    
    [SerializeField] private float movespeed;
    [SerializeField] private float jumpspeed;
    private float xInput;


    [Header("Attack info")]    
    [SerializeField] private float comboTime = .3f;
    [SerializeField] private float comboTimeWindow;
    private int comboCounter;


    [Header("Dash info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCoolDown;
    [SerializeField] private float dashCoolDownTimer;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        movespeed = 5;
        jumpspeed = 15;
        dashSpeed = 30;
        dashDuration = 0.15f;
        dashCoolDown = 1;
        comboTime = 1;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        dashTime -= Time.deltaTime;
        dashCoolDownTimer -= Time.deltaTime;
        
        comboTimeWindow -= Time.deltaTime;
        if (comboTimeWindow < 0)
        {
            comboCounter = 0;
        }

        Movement();
        CheckInput();
        FlipController();
        AnimatorControllers();

    }

    public void AttackOver()
    {
        isAttacking = false;
        comboCounter++;
        if (comboCounter > 2)
        {
            comboCounter = 0;
        }
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
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
            StartAttackEvent();
        }
    }

    private void StartAttackEvent()
    {
        if (!isGrounded) return;
        isAttacking = true;
        comboTimeWindow = comboTime;
    }

    private void DashAbility()
    {
        if (dashCoolDownTimer < 0&& !isAttacking)
        {
            dashCoolDownTimer = dashCoolDown;
            dashTime = dashDuration;
        }
    }

    private void Movement()
    {
        if (isAttacking)
        {
            rb.velocity = new Vector2(0, 0);
        }
        else if (dashTime > 0)
        {
            rb.velocity = new Vector2(facingDirection * dashSpeed, 0);
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

    private void FlipController()
    {
        if (rb.velocity.x > 0 && !facingRight)
            Flip();
        else if (rb.velocity.x < 0 && facingRight)
            Flip();
    }

    protected override void CollisionCheck()
    {
        base.CollisionCheck();
    }

}
///////////////////////////////////