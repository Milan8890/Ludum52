using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pmovement : MonoBehaviour
{
    private float Speed = 0.05f;
    private float JumpPower = 7f;
    private float airSpeed = 1f;

    public bool IsGrounded = true;
    public bool IsFacingRight = true;

    public Transform GroundCheck;

    float horMove = 0f;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsGrounded)
            airSpeed = 1f;
        else
            airSpeed = 0.8f;

        horMove = Input.GetAxis("Horizontal") * Speed * airSpeed;
        

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
            rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            
    }

    private void FixedUpdate()
    {
        if(!IsFacingRight)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;

        if (horMove > 0 && !IsFacingRight || horMove < 0 && IsFacingRight)
            IsFacingRight = !IsFacingRight;


        transform.Translate(horMove * Vector2.right);
    }
}
