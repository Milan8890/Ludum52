using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    public float speed = 5f;
    public float bulletLife = 0f;
    public int damage = 20;

    public GameObject player;
    public GameObject bullet;

    private void Start()
    {
        player = GameObject.Find("player");
       
        //Debug.Log(bulletLife);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Enemy"))
        {
            collision.GetComponent<Enemy>().getDamage(damage);
            collision.GetComponent<Enemy>().applyKnockback(transform.rotation);

            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (bulletLife-Time.deltaTime <= 0.0f)
            Destroy(gameObject);
        bulletLife -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        transform.Translate(speed * Vector2.right);
    }
}
