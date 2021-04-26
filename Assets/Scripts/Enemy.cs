using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int hp;
    public int attack;
    public Text namePlate;
    public Slider hpBar;

    public Character enemy;


    public void Attack()
    {
        enemy.hp -= attack;
    }
}
