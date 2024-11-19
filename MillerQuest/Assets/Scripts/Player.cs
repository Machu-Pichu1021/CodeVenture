using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D collider;

    [SerializeField] private float speed = 5f;

    //Jump stuff
    private const float gravityScale = 3;
    [SerializeField] private float jumpStrength = 12;
    [SerializeField] private int maxJumps = 2;
    private int jumpsRemaining;
    [SerializeField] private Transform groundCheckpoint;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheckpoint;

    private const float scale = 5;

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
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float velocityX = rb.velocity.x;
        if (isOnGround)
            velocityX = horizontalInput * speed;
       

        if (velocityX < 0)
            transform.localScale = new Vector3(-scale, scale, scale);
        else if (velocityX > 0)
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
            print("Jumping");
            velocityY = jumpStrength;
            if (isOnWall)
            {
                if (horizontalInput < 0)
                    velocityX = speed * .75f;
                else if (horizontalInput > 0)
                    velocityX = speed * -.75f;
            }
            jumpsRemaining--;
        }

        //Update Velocity
        rb.velocity = new Vector2(velocityX, velocityY);
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
