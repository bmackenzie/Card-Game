using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Character")]
public class PlayerCharacter : ScriptableObject
{
    public string characterName;
    public int attack;
    public int hp;
    public Sprite art;

    public List<Card> deck = new List<Card>();
    List<Card> discardPile = new List<Card>();
}
