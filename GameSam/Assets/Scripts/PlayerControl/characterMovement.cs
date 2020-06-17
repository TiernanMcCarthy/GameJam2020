using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rigidBody;
    public float speed;
    
    [Header("Movement variables")]
    public int playerDirection = 1;
    bool shouldFlip = false;
    public bool canMove;
    private float xMove;
    public bool isMoving;
    [Header("Sprites and visuals")]
    SpriteRenderer spriteRender;
    Animator animator;
    [Header("Ground checking variables")]
    public bool grounded;
    public Transform groundChecker;
    public LayerMask groundLayerMask;
    public float GroundCheckRadius;

    [Header("Jumping variables")]
    public float jumpForce;
    public float yVelocity; //the y velocity - used for animations
    public float groundedRemember; //how much time has passed since last being grounded
    public float groundedRememberTime = 0.25f; //how much time the player can still jump for after leaving the ground
    public float jumpPressRemember; //the amount of time since the jump button was last pressed
    public float jumpRememberTimer = 0.2f; //how long jump inputs should be remembered for
    public bool isJumping = false;

    [Header("Networking Variables")]
    public bool Player1;

    public characterMovement OtherPlayer;

    public GameObject Camera;

    public CheckPoint SpawnPoint;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        groundedRemember -= Time.deltaTime;
        if(grounded)
        {
            groundedRemember = groundedRememberTime;
        }
        jumpPressRemember -= Time.deltaTime;
        if(Input.GetButtonDown("Jump"))
        {
            jumpPressRemember = jumpRememberTimer;
        }
    }

    private void FixedUpdate()
    {
        checkSurroundings();
        if (canMove)
        {
            movePlayer();
            
        }
    }
    private void movePlayer()
    {
        if (Player1 == true)
        {
            xMove = Input.GetAxisRaw("Horizontal");
            if (xMove != 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
            animator.SetBool("isMoving", isMoving);
            if (xMove > 0.0f && playerDirection != 1)
            {
                flipPlayer();
            }
            else if (xMove < 0.0f && playerDirection != -1)
            {
                flipPlayer();
            }
            if (grounded)
            {
                rigidBody.velocity = new Vector3(xMove * speed, rigidBody.velocity.y, 0);
            }
            if (!grounded)
            {
                if (rigidBody.velocity.x == 0)
                {
                    rigidBody.velocity = new Vector3(xMove * 5, rigidBody.velocity.y, 0);
                }

            }
            //if (Input.GetButtonDown("Jump") && grounded)
            if((jumpPressRemember > 0) && (groundedRemember > 0) && !isJumping)
            {
                jump();
            }
        }
        else
        {

            if (rigidBody.velocity.x != 0)
            {
                isMoving = true;
                if (playerDirection != 1 && rigidBody.velocity.x > 0)
                {
                    flipPlayer();
                }
                else if (playerDirection != -1 && rigidBody.velocity.x < 0)
                {
                    flipPlayer();
                }

                animator.SetBool("isMoving", true);

            }
            else
            {
                isMoving = false;
                animator.SetBool("isMoving", false);
            }


        }
        yVelocity = rigidBody.velocity.y;
        animator.SetFloat("yVelocity", yVelocity);
        Camera.SetActive(Player1);
    }
    private void jump()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f,0f);
        rigidBody.velocity += Vector3.up * jumpForce;

    }
    private void flipPlayer()
    {
        playerDirection *= -1;
        shouldFlip = !shouldFlip;
        spriteRender.flipX = shouldFlip;
        if(!grounded)
        {
            rigidBody.velocity = new Vector3(0,rigidBody.velocity.y,0);
        }
        //transform.Rotate(0, 180.0f, 0);
    }
    private void checkSurroundings()
    {
        grounded = Physics.CheckSphere(groundChecker.position, GroundCheckRadius, groundLayerMask);
        animator.SetBool("isGrounded", grounded);
        if(grounded && rigidBody.velocity.y <=0)
        {
            isJumping = false;
        }
    }

    public void Respawn()
    {
        Debug.Log("Spongebob me boi I've snorted an entire suitcase of ketamine and I'm going to fucking die");
        if (SpawnPoint != null)
        {
            transform.position = SpawnPoint.transform.position;
            rigidBody.velocity = Vector3.zero;
        }
        else
        {
            transform.position = Vector3.zero;
            rigidBody.velocity = Vector3.zero;
        }
    }

}