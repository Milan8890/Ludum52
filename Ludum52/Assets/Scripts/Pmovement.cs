using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pmovement : MonoBehaviour
{
    public float Speed = 0.15f;
    public float JumpPower = 13f;

    public bool IsGrounded = true;
    public bool IsFacingRight = true;
    public bool canMove = true;

    public GameObject player;
    public float horMove = 0f;
    
    Rigidbody2D rb;

    public List<GameObject> limbs;

    private void Start()
    {
        player = GameObject.Find("player");
        rb = player.GetComponent<Rigidbody2D>();

        for(int i = 0; i < player.transform.Find("LIMBS").childCount; i++)
            limbs.Add(player.transform.Find("LIMBS").GetChild(i).gameObject);
    }

    void Update()
    {
        if (canMove)
        {
            horMove = Input.GetAxis("Horizontal") * Speed;

            if (Input.GetKey(KeyCode.Space) && IsGrounded)
            {
                IsGrounded = false;
                rb.velocity=Vector2.up * JumpPower;
            }
        }
    }

    private void FixedUpdate()
    {
        foreach(GameObject limb  in limbs)
            limb.GetComponent<SpriteRenderer>().flipX = !IsFacingRight;

        if (horMove > 0 && !IsFacingRight || horMove < 0 && IsFacingRight)
            IsFacingRight = !IsFacingRight;


        player.transform.Translate(horMove * Vector2.right);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
            IsGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
            IsGrounded = false;
    }
}