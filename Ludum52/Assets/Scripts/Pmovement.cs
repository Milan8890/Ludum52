using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pmovement : MonoBehaviour
{
    private float Speed = 1f;
    private float JumpPower = 10f;
    public bool IsGrounded = true;
    private bool IsFacingRight = true;

    public Rigidbody2D rb;
    public Transform GroundCheck;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) && IsGrounded)
        {
            IsFacingRight = true;
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) && IsGrounded)
        {
            IsFacingRight = false;
            rb.velocity = new Vector2(Speed * -1, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.Space) && IsGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpPower);
        }
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
