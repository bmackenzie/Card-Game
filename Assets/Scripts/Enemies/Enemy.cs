using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int hp;
    public int attack;
    public Sprite art;


    public Text namePlate;
    public Slider hpBar;
    public Image portrait;

    public PlayerCharacter enemy;


    public void Attack()
    {
        enemy.hp -= attack;
    }

    void Start()
    {
        portrait.sprite = art;
        namePlate.text = enemyName;

    }
}
