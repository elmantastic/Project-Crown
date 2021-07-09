using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float movementInputDirection;
    private float verticalInputDirection;
    private int amountOfJumpsLeft;
    private int facingDirection = 1;
    private float jumpCooldown;
    private float turnTimer;
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100f; // biar bisa langsung dash ketika game dimulai
    private float jumpDownTimer;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Animator anim;
    private SpriteRenderer sprt;

    private bool isFacingRight = true;
    private bool isRunning;
    private bool isGrounded;
    private bool isSwitchGrounded;
    private bool onGround;
    private bool isOnWall;
    private bool isWallSliding;
    private bool isTouchLedge;
    private bool isDashing;
    private bool isDamaged;
    private bool canClimbLedge = false;
    private bool ledgeDetected;
    private bool canJump;
    private bool canMove;
    private bool canFlip;
    private bool canThroughWall;

    public float movementSpeed = 10.0f;
    public float jumpForce = 10.0f;
    public int amountOfJumps = 1;
    public float groundCheckRadius;
    public float wallCheckDistance;
    public float wallSlidingSpeed;
    public float movementForceInAir;
    public float airDragMiltiplier = 0.95f;
    public float varJumpHeightMultiplier = 0.5f;
    public float wallHopForce;
    public float wallJumpForce;
    public float turnTimerSet;
    public float amountOfGoThroughWall = 1;
    public float dashTime;
    public float dashSpeed;
    public float dashCooldown;
    public float distanceBetweenImage; // jarak untuk after image ketika dash


    public float ledgeClimbXOffset1 = 0f;
    public float ledgeClimbYOffset1 = 0f;
    public float ledgeClimbXOffset2 = 0f;
    public float ledgeClimbYOffset2 = 0f;


    public Vector2 wallHopDirection;
    public Vector2 wallJumpDirection;
    private Vector2 ledgePostBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;


    public Transform groundCheck;
    public Transform wallCheck;
    public Transform ledgeCheck;
    public LayerMask whatIsGround;
    public LayerMask whatIsSwitchGround;

    private PlayerHealth health;
    private PlayerSkill skill;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprt = GetComponent<SpriteRenderer>();
        health = GetComponent<PlayerHealth>();
        skill = GetComponent<PlayerSkill>();
        amountOfJumpsLeft = amountOfJumps;

        // biar vectornya jadi 1 biar disa dikalikan dengan forcenya jadi hasilnya ttp forcenya
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimation();
        CheckLedgeClimb();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckDash();
        CheckIfCanGoThroughWall();
        CheckOnGround();
    }

    private void FixedUpdate() {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckInput(){
        movementInputDirection = Input.GetAxisRaw("Horizontal");
        verticalInputDirection = Input.GetAxisRaw("Vertical");

        if(jumpCooldown > 0.2f){
            if(Input.GetKeyDown(KeyCode.Space) && verticalInputDirection >= 0 ){
                Jump();
            }
            if(Input.GetKeyUp(KeyCode.Space) && verticalInputDirection >= 0){
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * varJumpHeightMultiplier);
            }

        } else {
            jumpCooldown += Time.deltaTime;
            if(Input.GetKeyUp(KeyCode.Space) && verticalInputDirection >= 0){
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * varJumpHeightMultiplier);
            }
        }

        if(Input.GetButtonDown("Horizontal") && isOnWall){
            if(!isGrounded && !isSwitchGrounded && movementInputDirection != facingDirection){
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
            }
        }

        if(verticalInputDirection < 0f && isSwitchGrounded && movementInputDirection == 0){
            if(Input.GetKeyDown(KeyCode.Space)){
                bc.isTrigger = true;
                jumpDownTimer = 0f;
            }
        }

        if(jumpDownTimer >= 0.3f){
            bc.isTrigger = false;
        } else {
            jumpDownTimer +=Time.deltaTime;
            
        }

        if(turnTimer >= 0){
            turnTimer -= Time.deltaTime;

            if(turnTimer <= 0){
                canMove = true;
                canFlip = true;
                isDamaged = false;
            }
        }

        if(Time.time >= (lastDash + dashCooldown)){
            canThroughWall = true;
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0f && !isOnWall){
            // forward scroll
            if(Time.time >= (lastDash + dashCooldown)){
                AttemptToDash();

            }
        }
    }

    private void CheckSurroundings(){
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        
        isSwitchGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsSwitchGround);

        isOnWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        isTouchLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, whatIsGround);

        if(isOnWall && !isTouchLedge && !ledgeDetected){
            ledgeDetected = true;
            ledgePostBot = wallCheck.position;
        }
    }

    private void CheckLedgeClimb(){
        
        if(ledgeDetected && !canClimbLedge && isOnWall){
            
            if(isFacingRight){
                ledgePos1 = new Vector2(Mathf.Floor(ledgePostBot.x + wallCheckDistance) - ledgeClimbXOffset1, Mathf.Floor(ledgePostBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePostBot.x + wallCheckDistance) + ledgeClimbXOffset2, Mathf.Floor(ledgePostBot.y) + ledgeClimbYOffset2);

            } else {
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePostBot.x - wallCheckDistance) + ledgeClimbXOffset1, Mathf.Floor(ledgePostBot.y) + ledgeClimbYOffset1);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePostBot.x - wallCheckDistance) - ledgeClimbXOffset2, Mathf.Floor(ledgePostBot.y) + ledgeClimbYOffset2);

            }

            if((Input.GetKeyDown(KeyCode.Space) && isGrounded) || !isGrounded){
                turnTimer = turnTimerSet;
                canClimbLedge = true;
                canMove = false;
                canFlip = false;
            }

            anim.SetBool("canClimbLedge", canClimbLedge);
        }

        if(canClimbLedge){
            transform.position = ledgePos1;

        }
    }

    public void FinishClimbLedge(){
        canClimbLedge = false;
        transform.position = ledgePos2;
        canMove = true;
        canFlip = true;
        ledgeDetected = false;
        anim.SetBool("canClimbLedge", canClimbLedge);

    }

    private void CheckIfWallSliding(){
        if(isOnWall && !isGrounded && movementInputDirection == facingDirection && !canClimbLedge && rb.velocity.y < 0){
            isWallSliding = true;
        } else {
            isWallSliding = false;
        }
    }

    private void CheckIfCanJump(){
        if(isGrounded || isSwitchGrounded){
            amountOfJumpsLeft = amountOfJumps;
        } else if (isWallSliding){
            amountOfJumpsLeft = amountOfJumps + 1;
        }

        if(amountOfJumpsLeft <= 0){
            canJump = false;
        } else {
            canJump = true;
        }
    }

    private void CheckIfCanGoThroughWall(){
        if(amountOfGoThroughWall > 0){
            canThroughWall = true;
        }

        if(amountOfGoThroughWall <= 0 && (isOnWall || isGrounded || isSwitchGrounded) && !isDashing){
            amountOfGoThroughWall = 1;
        }

    }

    private void Jump(){
        
        if(canJump && !isWallSliding){
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            GameManager.Instance.SoundPlayerJump();

            amountOfJumpsLeft--;
            jumpCooldown = 0f;
            

        } else if((isWallSliding || isOnWall) && movementInputDirection != facingDirection && movementInputDirection != 0 && canJump){ // hanya bisa jump ke belik arah dari wall
            
            rb.velocity = new Vector2(rb.velocity.x, 0);
            GameManager.Instance.SoundPlayerJump();

            jumpCooldown = 0f;
            isWallSliding = false;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
            amountOfJumpsLeft--;

            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);
        }

        /*
        else if (isWallSliding && movementInputDirection == 0 && canJump){
            //wall hop
            isWallSliding = false;
            amountOfJumpsLeft--;
            jumpCooldown = 0f;
            Vector2 forceToAdd = new Vector2(wallHopForce * wallHopDirection.x * -facingDirection, wallHopForce * wallHopDirection.y);
            rb.AddForce(forceToAdd, ForceMode2D.Impulse);

        }
        */

    }

    private void AttemptToDash(){
        isDashing = true;
        skill.Dashing();
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }

    private void CheckDash(){
        if(isDashing){

            if(dashTimeLeft > 0 ){
                canMove = false;
                canFlip = false;

                rb.velocity = new Vector2(dashSpeed * facingDirection, 0); // set the y to rb.velocity.y jika pengen bisa jatuh saat dash
                //rb.AddForce(new Vector2( 2.5f * facingDirection, 0), ForceMode2D.Impulse);
                dashTimeLeft -= Time.deltaTime;

                if(Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImage){
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }

            }

            if(dashTimeLeft <= 0){
                isDashing = false;
                canMove = true;
                canFlip = true;
            }
        }

    }

    private void ApplyMovement(){
        
        if(!isGrounded && !isSwitchGrounded && !isWallSliding && movementInputDirection == 0){
            rb.velocity = new Vector2(rb.velocity.x * airDragMiltiplier, rb.velocity.y);
        } else if (canMove) {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }

        /*
        if(isGrounded){
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }
        if(!isGrounded && !isWallSliding && movementInputDirection != 0){
            Vector2 forceToAdd = new Vector2(movementForceInAir * movementInputDirection, 0);
            rb.AddForce(forceToAdd);

            if(Mathf.Abs(rb.velocity.x)> movementSpeed){
                rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
            }
        }
        if(!isGrounded && !isWallSliding && movementInputDirection == 0){
            rb.velocity = new Vector2(rb.velocity.x * airDragMiltiplier, rb.velocity.y);
        }

        */

        if(isWallSliding){
            if(rb.velocity.y < -wallSlidingSpeed){
                rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
            }

        }
    }

    private void CheckMovementDirection(){
        if(isFacingRight && movementInputDirection < 0){
            Flip();
        } else if (!isFacingRight && movementInputDirection > 0){
            Flip();
        }

        //if (movementInputDirection != 0){
        if (Mathf.Abs(rb.velocity.x) >= 0.01f){
            isRunning = true;
        } else {
            isRunning = false;  
        } 
    }

    private void CheckOnGround(){
        if(isGrounded || isSwitchGrounded){
            onGround = true;
        } else {
            onGround = false;
        }
    }

    private void UpdateAnimation(){
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isGrounded", onGround);
        anim.SetBool("isSwitchGrounded", isSwitchGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);

        /*
        if(isWallSliding || isDamaged){
            sprt.color = Color.red;
        } else if (!isWallSliding && movementInputDirection != 0){
            sprt.color = Color.blue;
        } else if (isDashing){
            sprt.color = Color.yellow;
        } else {
            sprt.color = Color.white;
        }
        */
        if(isDamaged){
            sprt.color = Color.red;
        } else {
            sprt.color = Color.white;
        }
    }

    private void Flip(){
        if(!isWallSliding && canFlip){
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f, 0.0f);
        }
    }

    private void DisbleFlip(){
        canFlip = false;
    }

    private void EnableFlip(){
        canFlip = true;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Terrain"){
            if(isDashing && canThroughWall){

                var myTerrain = collision.gameObject.GetComponent<Terrain>();
                myTerrain.SetTrigger();
                amountOfGoThroughWall--;
                canThroughWall = false;

                //Vector2 impulse = new Vector2(-30, 20);
                //rb.AddForce(impulse, ForceMode2D.Impulse);
                
                // collision.gameObject.SetActive(false);
            }
            
        }

        if(collision.gameObject.tag == "Spike"){
            rb.velocity = Vector2.zero;
            isDamaged = true;
            canFlip = false;
            canMove = false;
            amountOfJumpsLeft = amountOfJumps;
            turnTimer = turnTimerSet;
            
            Vector2 impulse = new Vector2(5 * -facingDirection, 12);
            rb.AddForce(impulse, ForceMode2D.Impulse);

            // take damage
            health.TakeDamage(2);
        }


        if(collision.gameObject.tag == "MovingTerrain"){
            transform.parent = collision.gameObject.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "MovingTerrain"){
            transform.parent = null;
        }
    }

}
