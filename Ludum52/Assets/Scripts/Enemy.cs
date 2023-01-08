using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int Hp = 100;
    private int damage = 10;
    private float knockbackStrength = 3.0f;

    public GameObject enemyObject;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //viselkedés
    }
    public void getDamage(int dmg)
    {
        Hp -= dmg;
        if (Hp <= 0)
            Destroy(enemyObject);
    }
    public void applyKnockback(in Quaternion projectileRotation)
    {
        Vector3 eulerAngles = projectileRotation.eulerAngles;
        Vector2 direction = new Vector2(Mathf.Cos(eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(eulerAngles.z * Mathf.Deg2Rad));
        applyKnockback(direction);
    }
    public void applyKnockback(in Vector2 direction)
    {
        direction.Normalize();
        rb.AddForce(direction * knockbackStrength + Vector2.up * 1.0f, ForceMode2D.Impulse);
    }
}
