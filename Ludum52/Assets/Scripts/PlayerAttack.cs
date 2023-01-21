using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Arm
{
    public int maxammo;
    public int ammo;
    public int weaponType;
    public float reloadSpeed;

    public float attackDelay;
    public float attackDuration;

    public float range;
    
    public float damageCollDistance;
    public bool melee ;
    public int damage;

    public bool attacking = false;
    public bool canAttack = true;
}

public class PlayerAttack : MonoBehaviour
{
    public Arm Larm;
    public Arm Rarm;

    public GameObject player;
    public GameObject[] bullet;
    public Transform barrelEnd;

    public int maxHp = 100;
    public float Hp = 100f;

    public Image damageCooldownUI;

    public float angle;

    public GameObject L;
    public GameObject R;

    private void Start()
    {

    }
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));


        if (Input.GetKey(KeyCode.R) && Larm.ammo!=Larm.maxammo && Rarm.ammo != Rarm.maxammo)
            StartCoroutine(Reload());


        if (Input.GetKey(KeyCode.Mouse0) && Larm.canAttack && Larm.ammo!=0)
        {

            Larm.canAttack = false;

            if (Larm.melee)
                StartCoroutine(LMattack());
            else
            {
                StartCoroutine(LRattack());
            }
        }

        if (Input.GetKey(KeyCode.Mouse1) && Rarm.canAttack && Rarm.ammo != 0)
        {
            Rarm.canAttack = false;

            if (Rarm.melee)
                StartCoroutine(RMattack());
            else
            {
                StartCoroutine(RRattack());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Larm.attacking && Larm.melee)
        {
            if (collision.gameObject.tag=="Enemy")
            {
                collision.GetComponent<Enemy>().getDamage(Larm.damage);
                collision.GetComponent<Enemy>().applyKnockback(transform.rotation);
            }

            Larm.attacking = false;
        }

        if (Rarm.attacking && Rarm.melee)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.GetComponent<Enemy>().getDamage(Rarm.damage);
                collision.GetComponent<Enemy>().applyKnockback(transform.rotation);
            }

            Rarm.attacking = false;
        }
    }
    IEnumerator LMattack()
    {
        damageCooldownUI.color = new Color(1, 0, 0);

        Larm.attacking = true;

        yield return new WaitForSeconds(Larm.attackDuration);
        Larm.attacking = false;

        yield return new WaitForSeconds(Larm.attackDelay);

        Larm.canAttack = true;
        damageCooldownUI.color = new Color(0, 1, 0);
    }
    IEnumerator LRattack()
    {
        damageCooldownUI.color = new Color(1, 0, 0);
        switch(Larm.weaponType)
        {
            case 1:
                //pistol/sniper
                
                L = Instantiate(bullet[Larm.weaponType], barrelEnd.position, barrelEnd.rotation);
                L.GetComponent<playerBullet>().bulletLife = Larm.range / L.GetComponent<playerBullet>().speed;
                L.GetComponent<playerBullet>().damage = Larm.damage;
                Larm.ammo--;
                break;
            case 2:
                //shotgun
                int pelletCount=10;
                float pelletSway=35f;
                
                for (int i=0; i<pelletCount; i++)
                {

                    barrelEnd.rotation = Quaternion.Euler(new Vector3(0, 0, angle + Random.Range(-pelletSway / 2, pelletSway / 2)));
                    

                    L=Instantiate(bullet[Larm.weaponType], barrelEnd.position, barrelEnd.rotation);
                    L.GetComponent<playerBullet>().bulletLife = Random.Range(0, Larm.range) / L.GetComponent<playerBullet>().speed;
                    L.GetComponent<playerBullet>().damage = Larm.damage/pelletCount;


                }

                barrelEnd.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                Larm.ammo--;
                break;
            case 3:
                //burst rifle
                
                for (int i = 0; i < 3; i++)
                {
                    float accuracy = 5f;
                    barrelEnd.rotation = Quaternion.Euler(new Vector3(0, 0, angle + Random.Range(-accuracy / 2, accuracy / 2)));

                    L =Instantiate(bullet[Larm.weaponType], barrelEnd.position, barrelEnd.rotation);
                    L.GetComponent<playerBullet>().bulletLife =Larm.range / L.GetComponent<playerBullet>().speed;
                    L.GetComponent<playerBullet>().damage = Larm.damage;

                    yield return new WaitForSeconds(Larm.attackDuration / 10);
                }
                barrelEnd.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                Larm.ammo -= 3;
                break;
        }

        

        yield return new WaitForSeconds(Larm.attackDelay);

        Larm.canAttack = true;
        if(Larm.ammo !=0)
            damageCooldownUI.color = new Color(0, 1, 0);
    }

    IEnumerator RMattack()
    {
        damageCooldownUI.color = new Color(1, 0, 0);

        Rarm.attacking = true;

        yield return new WaitForSeconds(Rarm.attackDuration);
        Rarm.attacking = false;

        yield return new WaitForSeconds(Rarm.attackDelay);

        Rarm.canAttack = true;
        damageCooldownUI.color = new Color(0, 1, 0);
    }
    IEnumerator RRattack()
    {

        damageCooldownUI.color = new Color(1, 0, 0);
        switch (Rarm.weaponType)
        {
            case 1:
                //pistol/sniper
                R=Instantiate(bullet[Rarm.weaponType], barrelEnd.position, barrelEnd.rotation);
                R.GetComponent<playerBullet>().bulletLife =  Rarm.range / R.GetComponent<playerBullet>().speed;
                R.GetComponent<playerBullet>().damage = Rarm.damage;

                Rarm.ammo--;
                break;
            case 2:
                //shotgun
                int pelletCount = 10;
                float pelletSway = 35f;
                for (int i = 0; i < pelletCount; i++)
                {

                    barrelEnd.rotation = Quaternion.Euler(new Vector3(0, 0, angle + Random.Range(-pelletSway / 2, pelletSway / 2)));

                    R=Instantiate(bullet[Rarm.weaponType], barrelEnd.position, barrelEnd.rotation);
                    R.GetComponent<playerBullet>().bulletLife = Random.Range(0, Rarm.range) / R.GetComponent<playerBullet>().speed;
                    R.GetComponent<playerBullet>().damage = Rarm.damage / pelletCount;
                    //Debug.Log(range);
                }
                barrelEnd.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                Rarm.ammo--;
                break;
            case 3:
                //burst rifle
                for (int i = 0; i < 3; i++)
                {
                    float accuracy = 5f;
                    barrelEnd.rotation = Quaternion.Euler(new Vector3(0, 0, angle + Random.Range(-accuracy / 2, accuracy / 2)));

                    R=Instantiate(bullet[Rarm.weaponType], barrelEnd.position, barrelEnd.rotation);
                    R.GetComponent<playerBullet>().bulletLife =Rarm.range / R.GetComponent<playerBullet>().speed;
                    R.GetComponent<playerBullet>().damage = Rarm.damage;

                    yield return new WaitForSeconds(Rarm.attackDuration / 10);
                }
                barrelEnd.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                Rarm.ammo -= 3;
                break;
        }



        yield return new WaitForSeconds(Rarm.attackDelay);

        Rarm.canAttack = true;
        if (Rarm.ammo != 0)
            damageCooldownUI.color = new Color(0, 1, 0);
    }

    IEnumerator Reload()
    {
        Larm.canAttack = false;
        Rarm.canAttack = false;
        damageCooldownUI.color = new Color(1, 0, 0);
        Larm.ammo = Larm.maxammo;
        Rarm.ammo = Rarm.maxammo;


        yield return new WaitForSeconds(Rarm.reloadSpeed + Larm.reloadSpeed);

        Larm.canAttack = true;
        Rarm.canAttack = true;
        damageCooldownUI.color = new Color(0, 1, 0);
    }
}
