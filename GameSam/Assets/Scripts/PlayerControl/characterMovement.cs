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

    public bool Player1;

    public characterMovement OtherPlayer;

    public GameObject Camera;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            if (Input.GetButtonDown("Jump") && grounded)
            {

                jump();
            }
        }
        else
        {

            if (rigidBody.velocity.x != 0)
            {

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
                animator.SetBool("isMoving", false);
            }


        }
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
    }

}