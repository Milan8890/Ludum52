using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int Hp = 100;
    private int damage = 10;
    public GameObject enemyObject;
    // Update is called once per frame
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
}
