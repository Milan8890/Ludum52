using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeAttack : MonoBehaviour
{
    private bool attacking = false;
    private bool canAttack = true;
    private float attackDelay = 1f;
    private float attackDuration = 1f;
    private int damage = 50;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            StartCoroutine(attack());
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (attacking)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Enemy>().getDamage(damage);
            }
            // sebzést ide írni.
            attacking = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attacking)
        {
            if(collision.gameObject.tag=="Enemy")
            {
                collision.gameObject.GetComponent<Enemy>().getDamage(damage);
            }
            // sebzést ide írni.
            attacking = false;
        }
    }
    IEnumerator attack()
    {

        canAttack = false;
        attacking = true;
        yield return new WaitForSeconds(attackDuration);
        attacking = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }
}
