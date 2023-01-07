using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class meleeAttack : MonoBehaviour
{
    private bool attacking = false;
    private bool canAttack = true;
    private float attackDelay = 0.5f;
    private float attackDuration = 0.5f;
    [SerializeField] int damage = 50;


    public Image damageCooldownUI;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            StartCoroutine(attack());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attacking)
        {
            if (collision.gameObject.tag=="Enemy")
                collision.gameObject.GetComponent<Enemy>().getDamage(damage);

            attacking = false;
        }
    }
    IEnumerator attack()
    {
        damageCooldownUI.color = new Color(1, 0, 0);

        canAttack = false;
        attacking = true;

        yield return new WaitForSeconds(attackDuration);
        attacking = false;

        yield return new WaitForSeconds(attackDelay);
        
        canAttack = true;
        damageCooldownUI.color = new Color(0, 1, 0);
    }
}
