using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private bool attacking = false;
    private bool canAttack = true;
    private float attackDelay = 0.5f;
    private float attackDuration = 0.5f;
    private float angle;
    public float damageCollDistance = 0.34f;
    public bool melee = true;
    [SerializeField] int damage = 50;
    public GameObject player;
    public GameObject bullet;
    public Transform barrelEnd;

    public Image damageCooldownUI;
    private void Start()
    {
        //fix?
        //GetComponent<BoxCollider2D>().offset = new Vector2(damageCollDistance, 0);
    }
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        if (Input.GetKey(KeyCode.Mouse0) && canAttack)
        {
            canAttack = false;

            if (melee)
                StartCoroutine(Mattack());
            else
            {
                StartCoroutine(Rattack());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attacking && melee)
        {
            if (collision.gameObject.tag=="Enemy")
                collision.gameObject.GetComponent<Enemy>().getDamage(damage);

            attacking = false;
        }
    }
    IEnumerator Mattack()
    {
        damageCooldownUI.color = new Color(1, 0, 0);

        attacking = true;

        yield return new WaitForSeconds(attackDuration);
        attacking = false;

        yield return new WaitForSeconds(attackDelay);
        
        canAttack = true;
        damageCooldownUI.color = new Color(0, 1, 0);
    }
    IEnumerator Rattack()
    {
        damageCooldownUI.color = new Color(1, 0, 0);

        Instantiate(bullet, barrelEnd.position, barrelEnd.rotation);

        yield return new WaitForSeconds(attackDelay);

        canAttack = true;
        damageCooldownUI.color = new Color(0, 1, 0);
    }
}
