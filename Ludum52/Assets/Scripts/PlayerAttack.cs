using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public int weaponType = 2;
    private int maxammo = 30;
    private int ammo = 0;
    private int reloadSpeed = 2;
    private bool attacking = false;
    private bool canAttack = true;
    private float attackDelay = 0.5f;
    private float attackDuration = 0.5f;
    public float range = 0.5f; //ezt elvileg megkapjuk a partScriptbõl,de nemtom hogy hogyan.
    private float maxrange = 0.5f;
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
        ammo = maxammo;
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

        if (Input.GetKey(KeyCode.R) && ammo!=maxammo)
        {
            StartCoroutine(Reload());
        }
        if (Input.GetKey(KeyCode.Mouse0) && canAttack && ammo!=0)
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
            {
                collision.GetComponent<Enemy>().getDamage(damage);
                collision.GetComponent<Enemy>().applyKnockback(transform.rotation);
            }

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
        switch(//ide kéne a fegyver typeja, csak nemtom honnan lehet elérni
               weaponType)
        {
            case 1:
                //pistol/sniper
                Instantiate(bullet, barrelEnd.position, barrelEnd.rotation);
                ammo--;
                break;
            case 2:
                //shotgun
                int pelletCount=10;
                float pelletSway=35f;
                for (int i=0; i<pelletCount; i++)
                {

                    barrelEnd.rotation = Quaternion.Euler(new Vector3(0, 0, angle + Random.Range(-pelletSway / 2, pelletSway / 2)));
                    

                    Instantiate(bullet, barrelEnd.position, barrelEnd.rotation);
                    //Debug.Log(range);
                }
                barrelEnd.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                ammo--;
                break;
            case 3:
                //burst rifle
                for (int i = 0; i < 3; i++)
                {
                    float accuracy = 5f;
                    barrelEnd.rotation = Quaternion.Euler(new Vector3(0, 0, angle + Random.Range(-accuracy / 2, accuracy / 2)));

                    Instantiate(bullet, barrelEnd.position, barrelEnd.rotation);
                    yield return new WaitForSeconds(attackDuration / 10);
                }
                barrelEnd.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                ammo -= 3;
                break;
        }

        

        yield return new WaitForSeconds(attackDelay);

        canAttack = true;
        if(ammo!=0)
            damageCooldownUI.color = new Color(0, 1, 0);
    }
    IEnumerator Reload()
    {
        canAttack = false;
        damageCooldownUI.color = new Color(1, 0, 0);
        ammo = maxammo;
        yield return new WaitForSeconds(reloadSpeed);
        canAttack = true;
        damageCooldownUI.color = new Color(0, 1, 0);
    }
}
