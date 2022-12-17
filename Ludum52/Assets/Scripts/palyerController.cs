using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class palyerController : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.D))
            rb.AddForce(new Vector2(3,0));
        else if(Input.GetKey(KeyCode.A))
            rb.AddForce(new Vector2(-3, 0));
        else if (Input.GetKey(KeyCode.S))
            rb.AddForce(new Vector2(0, -3));
        else if (Input.GetKey(KeyCode.W))
            rb.AddForce(new Vector2(0, 3));
    }
}
