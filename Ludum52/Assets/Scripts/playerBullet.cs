using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    public float speed = 10f;
    public float bulletLife = 2.0f;
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
<<<<<<< Updated upstream
            collision.GetComponent<Enemy>().applyKnockback(transform.rotation);
=======
            Destroy()
>>>>>>> Stashed changes
        }
    }
    private void Update()
    {
        if ((bulletLife -= Time.deltaTime) <= 0.0f)
            Destroy(gameObject);
    }
    private void FixedUpdate()
    {  
        transform.Translate(speed * Vector2.right);
    }
}
