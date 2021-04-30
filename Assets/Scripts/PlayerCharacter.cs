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

    public List<Card> deckData = new List<Card>();
    public List<GameObject> deck = new List<GameObject>();
    public List<GameObject> discardPile = new List<GameObject>();
}
