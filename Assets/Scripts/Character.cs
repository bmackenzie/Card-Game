using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public string characterName;
    public int hp;
    public int attack;
    public Sprite art;

    public Text namePlate;
    public Image portrait;

    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();

    public void setCardOwner()
    {
        //set cards from this characters deck as owned by this character so their stats can be used. 
    }
}
