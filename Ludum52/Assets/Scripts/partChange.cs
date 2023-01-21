using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class partChange : MonoBehaviour
{
    public GameObject partPanel;
    public GameObject unusedPartsPanel;

    public GameObject[] locks;
    public List<GameObject> lockedParts;


    private void Start()
    { 
        partPanel.SetActive(false);
    }

    int lastChildCount = 0;

    void Update()
    {
        if (partPanel.activeSelf)
        {
            GetComponentInChildren<PlayerAttack>().enabled = false;
            Time.timeScale = 0f;
        }     
        else
        {
            GetComponentInChildren<PlayerAttack>().enabled = true;
            Time.timeScale = 1f;
        }
            

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (partPanel.activeSelf)
            {
                lockedParts.Clear();
                foreach (GameObject Lock in locks)
                    if(Lock.transform.childCount != 0)
                        lockedParts.Add(Lock.transform.GetChild(0).gameObject);
            }
            if(partPanel.activeSelf && lockedParts.Count == 0)
                change();

            if (lockedParts.Count == 5 || !partPanel.activeSelf)
            {
                partPanel.SetActive(!partPanel.activeSelf);
            }    
                

        }
            

        if (lastChildCount != unusedPartsPanel.transform.childCount && unusedPartsPanel.transform.childCount%2 == 1)
            unusedPartsPanel.GetComponent<RectTransform>().position -= new Vector3(0, 130, 0);

        lastChildCount = unusedPartsPanel.transform.childCount;
    }


 
    void change()
    {
        for (int i = 0; i < GetComponentInChildren<Pmovement>().limbs.Count; i++)
            GetComponentInChildren<Pmovement>().limbs[i].GetComponent<SpriteRenderer>().sprite = lockedParts[i].GetComponent<partScript>().part.sprite;

        GetComponentInChildren<Pmovement>().Speed = lockedParts[4].GetComponent<partScript>().part.speed;
        GetComponentInChildren<Pmovement>().JumpPower = lockedParts[4].GetComponent<partScript>().part.jumppower;

        GetComponentInChildren<PlayerAttack>().maxHp = lockedParts[0].GetComponent<partScript>().part.hp + lockedParts[1].GetComponent<partScript>().part.hp;

        GetComponentInChildren<PlayerAttack>().Larm.maxammo = lockedParts[2].GetComponent<partScript>().part.magazineSize;
        GetComponentInChildren<PlayerAttack>().Larm.ammo = lockedParts[2].GetComponent<partScript>().part.magazineSize;
        GetComponentInChildren<PlayerAttack>().Larm.weaponType = lockedParts[2].GetComponent<partScript>().part.typeSquared;
        GetComponentInChildren<PlayerAttack>().Larm.reloadSpeed = lockedParts[2].GetComponent<partScript>().part.reloadTime * lockedParts[0].GetComponent<partScript>().part.reloadSpeed;
        GetComponentInChildren<PlayerAttack>().Larm.attackDelay = lockedParts[2].GetComponent<partScript>().part.attackDelay;
        GetComponentInChildren<PlayerAttack>().Larm.attackDuration = lockedParts[2].GetComponent<partScript>().part.attackDuration;
        GetComponentInChildren<PlayerAttack>().Larm.range = lockedParts[2].GetComponent<partScript>().part.range;
        GetComponentInChildren<PlayerAttack>().Larm.damageCollDistance = lockedParts[2].GetComponent<partScript>().part.reach;
        GetComponentInChildren<PlayerAttack>().Larm.melee = lockedParts[2].GetComponent<partScript>().part.melee;
        GetComponentInChildren<PlayerAttack>().Larm.damage = lockedParts[2].GetComponent<partScript>().part.damage;


        GetComponentInChildren<PlayerAttack>().Rarm.maxammo = lockedParts[3].GetComponent<partScript>().part.magazineSize;
        GetComponentInChildren<PlayerAttack>().Rarm.ammo = lockedParts[3].GetComponent<partScript>().part.magazineSize;
        GetComponentInChildren<PlayerAttack>().Rarm.weaponType = lockedParts[3].GetComponent<partScript>().part.typeSquared;
        GetComponentInChildren<PlayerAttack>().Rarm.reloadSpeed = lockedParts[3].GetComponent<partScript>().part.reloadTime * lockedParts[0].GetComponent<partScript>().part.reloadSpeed;
        GetComponentInChildren<PlayerAttack>().Rarm.attackDelay = lockedParts[3].GetComponent<partScript>().part.attackDelay;
        GetComponentInChildren<PlayerAttack>().Rarm.attackDuration = lockedParts[3].GetComponent<partScript>().part.attackDuration;
        GetComponentInChildren<PlayerAttack>().Rarm.range = lockedParts[3].GetComponent<partScript>().part.range;
        GetComponentInChildren<PlayerAttack>().Rarm.damageCollDistance = lockedParts[3].GetComponent<partScript>().part.reach;
        GetComponentInChildren<PlayerAttack>().Rarm.melee = lockedParts[3].GetComponent<partScript>().part.melee;
        GetComponentInChildren<PlayerAttack>().Rarm.damage = lockedParts[3].GetComponent<partScript>().part.damage;

    }   
}