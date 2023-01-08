using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pmovement : MonoBehaviour
{
    private float Speed = 0.2f;
    private float JumpPower = 7f;
    private float airSpeed = 1f;

    public bool IsGrounded = true;
    public bool IsFacingRight = true;

    public Transform GroundCheck;


    bool jump = false;

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
            airSpeed = 0.5f;

        horMove = Input.GetAxis("Horizontal") * Speed * airSpeed;
        

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
            rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if (horMove > 0 && !IsFacingRight || horMove < 0 && IsFacingRight)
        {
            IsFacingRight = !IsFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x = localScale.x*-1;
            transform.localScale = localScale;
        }
        transform.Translate(horMove * Vector2.right);
        
    }
}
