using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBullet : MonoBehaviour
{
    public float speed = 1f;
    private float bulletLife = 0f;
    public int damage = 20;

    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("player");
        if(GameObject.Find("Attackcollider").GetComponent<PlayerAttack>().weaponType==2)
            bulletLife = Random.Range(GameObject.Find("Attackcollider").GetComponent<PlayerAttack>().range / 2, GameObject.Find("Attackcollider").GetComponent<PlayerAttack>().range) / speed;
        else
            bulletLife = GameObject.Find("Attackcollider").GetComponent<PlayerAttack>().range / speed;
        Debug.Log(bulletLife);
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
