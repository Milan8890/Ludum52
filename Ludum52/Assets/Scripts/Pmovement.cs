using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pmovement : MonoBehaviour
{
    public float Speed = 0.1f;
    public float JumpPower = 7f;
    public float airSpeed = 1f;

    public float maxHp = 0;
    public float hp = 0;

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
        

        if (Input.GetKey(KeyCode.Space) && IsGrounded)
        {
            IsGrounded = false;
            rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        }
            
    }

    private void FixedUpdate()
    {
        foreach(GameObject limb  in limbs)
            limb.GetComponent<SpriteRenderer>().flipX = !IsFacingRight;

        if (horMove > 0 && !IsFacingRight || horMove < 0 && IsFacingRight)
            IsFacingRight = !IsFacingRight;


        transform.Translate(horMove * Vector2.right);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
            IsGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
            IsGrounded = false;
    }
}