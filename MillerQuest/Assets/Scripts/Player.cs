using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    private Rigidbody2D rb;
    private BoxCollider2D collider;


    //Movement variables
    private float horizontalInput;
    private KeyCode lastMoveKeyPressed;
    private float timeRunning;
    [SerializeField] private float moveSpeed = 12.5f;

    //Jump stuff
    private const float gravityScale = 3;
    [SerializeField] private float jumpStrength = 12;
    [SerializeField] private int maxJumps = 2;
    private int jumpsRemaining;
    [SerializeField] private Transform groundCheckpoint;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheckpoint;

    private const float scale = 5;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        //Check if you're against the wall
        bool isOnWall = IsOnWall();

        //Check if you're on the ground
        bool isOnGround = IsOnGround();

        //Moving
        horizontalInput = Input.GetAxis("Horizontal");
        float velocityX = CalculateVelocityX(isOnGround, isOnWall);
        if (Input.GetKey(KeyCode.A))
            lastMoveKeyPressed = KeyCode.A;
        else if (Input.GetKey(KeyCode.D))
            lastMoveKeyPressed = KeyCode.D;
        else
            lastMoveKeyPressed = KeyCode.None;


        if (horizontalInput < 0)
            transform.localScale = new Vector3(-scale, scale, scale);
        else if (horizontalInput > 0)
            transform.localScale = Vector3.one * scale;

        //Jumping
        if (isOnGround)
            jumpsRemaining = maxJumps;
        else if (jumpsRemaining == maxJumps || isOnWall)
            jumpsRemaining = maxJumps - 1;

        float velocityY = rb.velocity.y;
        if (isOnWall && velocityY < 0)
            rb.gravityScale = gravityScale / 10;
        else
            rb.gravityScale = gravityScale;

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && jumpsRemaining > 0)
        {
            velocityY = CalculateVelocityY(velocityX, isOnGround, isOnWall);
            if (isOnWall)
            {
                if (horizontalInput < 0)
                    velocityX = moveSpeed * .75f;
                else if (horizontalInput > 0)
                    velocityX = moveSpeed * -.75f;
            }
            jumpsRemaining--;
        }

        //Update Velocity
        rb.velocity = new Vector2(velocityX, velocityY);
    }

    private float CalculateVelocityX(bool isOnGround, bool isOnWall)
    {
        if (PressedKeyChanged() && isOnGround)
        {
            timeRunning = 0;
            return moveSpeed * horizontalInput;
        }
        else if (isOnGround)
        {
            timeRunning += Time.deltaTime;
            if (horizontalInput == 0 || isOnWall)
                timeRunning = 0;
            float velocity = timeRunning * moveSpeed * horizontalInput;
            velocity = Mathf.Clamp(velocity, -moveSpeed, moveSpeed);
            return velocity;
        }
        else
            return rb.velocity.x;
    }

    private bool PressedKeyChanged()
    {
        return (lastMoveKeyPressed == KeyCode.D && Input.GetKey(KeyCode.A)) || (lastMoveKeyPressed == KeyCode.A && Input.GetKey(KeyCode.D));
    }

    private float CalculateVelocityY(float velocityX, bool isOnGround, bool isOnWall)
    {
        velocityX = Mathf.Abs(velocityX);
        return isOnGround ? (isOnWall ? jumpStrength : velocityX > 5 ? (jumpStrength * (velocityX / 10)) : jumpStrength * .5f) : jumpStrength;
    }

    private bool IsOnGround()
    {
        return Physics2D.OverlapBox(groundCheckpoint.position, new Vector2(collider.size.x - 0.5f, .1f), 0, groundLayer);
    }

    public bool IsOnWall()
    {
        return Physics2D.OverlapBox(wallCheckpoint.position, new Vector2(.1f, collider.size.y - 0.5f), 0, groundLayer)
            && ((transform.localScale.x < 0 && Input.GetKey(KeyCode.A)) || (transform.localScale.x > 0 && Input.GetKey(KeyCode.D)));
    }
}
