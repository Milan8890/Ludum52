using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pmovement : MonoBehaviour
{
    private float Speed = 0.1f;
    private float JumpPower = 7f;
    private float airSpeed = 1f;

    public bool IsGrounded = true;
    public bool IsFacingRight = true;

    public Transform GroundCheck;

    float horMove = 0f;
    Rigidbody2D rb;

    public List<GameObject> limbs;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        for(int i = 0; i < transform.Find("LIMBS").childCount; i++)
            limbs.Add(transform.Find("LIMBS").GetChild(i).gameObject);
    }

    void Update()
    {
        if (IsGrounded)
            airSpeed = 1f;
        else
            airSpeed = 0.8f;

        horMove = Input.GetAxis("Horizontal") * Speed * airSpeed;
        //horMove = Input.GetAxisRaw("Horizontal") * Speed * airSpeed; nem tudom eldönteni melyik a jobb
        

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
            rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        foreach(GameObject limb  in limbs)
            limb.GetComponent<SpriteRenderer>().flipX = !IsFacingRight;

        if (horMove > 0 && !IsFacingRight || horMove < 0 && IsFacingRight)
            IsFacingRight = !IsFacingRight;


        transform.Translate(horMove * Vector2.right);
    }
}