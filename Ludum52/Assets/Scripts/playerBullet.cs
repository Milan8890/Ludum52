using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 20;

    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("player");
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Enemy"))
        {
            collision.GetComponent<Enemy>().getDamage(damage);
        }
    }
    private void FixedUpdate()
    {  
        transform.Translate(speed * Vector2.right);
    }
}
