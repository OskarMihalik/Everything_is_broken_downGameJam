using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Rigidbody2D rg;
    public float horizontalSpeed = 0f;
    public float jumpForce = 5f;
    public GroundCheckerController groundCheckController;
    public float fallMultiplier = 2.5f; 
    public float lowJumpMultiplier = 2f;
    public bool canMove = true;
    public float dashforce;

    private Vector3 playerStartingPos;

    private void Start()
    {
        playerStartingPos = transform.position;
    }

    void Update()
    {
        if (canMove)
        {
            Move();
            Jump();
            BetterJump();
            Dash();
        }

    }
    
    public void MovePlayerAtStartOfLevel()
    {
        transform.position = playerStartingPos;
    }
    public void StopMoving()
    {
        canMove = false;
        rg.velocity = new Vector2(0f, 0f); 
    }

    public void ResumeMoving()
    {
        canMove = true;
    }

    private void Move() { 
        float x = Input.GetAxisRaw("Horizontal"); 
        float moveBy = x * horizontalSpeed; 
        rg.velocity = new Vector2(moveBy, rg.velocity.y); 
    }

    private void Jump()
    {
        if (Input.GetAxisRaw("Jump") > 0 && groundCheckController.isGrounded)
        {
            rg.velocity = new Vector2(rg.velocity.x, jumpForce);
            groundCheckController.isGrounded = false;
        }
    }
    
    private void BetterJump() {
        if (rg.velocity.y < 0) {
            rg.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rg.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
            rg.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }   
    }

    private void Dash()
    {
        float x = Input.GetAxisRaw("Horizontal"); 
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rg.velocity = new Vector2(rg.velocity.y, dashforce);
            if (x > 0)
            {
                rg.velocity = new Vector2(dashforce, rg.velocity.y); 
            }
            else
            {
                rg.velocity = new Vector2(-dashforce, rg.velocity.y); 
            }
        }
    }
    
}
