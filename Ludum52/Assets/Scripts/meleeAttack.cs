using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeAttack : MonoBehaviour
{
    private bool attacking = false;
    private bool canAttack = true;
    private float attackDelay = 1f;
    private float attackDuration = 0.1f;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            StartCoroutine(attack());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (attacking)
        {
            // sebzést ide írni.
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
