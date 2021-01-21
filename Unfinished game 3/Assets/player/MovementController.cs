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
    
    bool isGrounded = false; 
    public Transform isGroundedChecker; 
    public float checkGroundRadius; 
    public LayerMask groundLayer;
    void Update()
    {
        Move();
        Jump();
        BetterJump();
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
    
    void BetterJump() {
        if (rg.velocity.y < 0) {
            rg.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rg.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) {
            rg.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }   
    }
    
}
