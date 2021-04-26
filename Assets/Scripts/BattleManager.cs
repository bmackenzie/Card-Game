using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// game now draws from the characters deck, next steps: cards drawn are actually removed from the deck, instead of destroying cards, place them in an inaccessible discard pile the loops back into the deck when it's empty.
public enum BattleState { START, PLAYERTURN, ENEMYTURN, ESCAPE, VICTORY, DEFEAT, RUN }

public class BattleManager : MonoBehaviour
{
    public BattleState state;

    public GameObject card;
    public GameObject playerArea;
    public GameObject character;
    public GameObject dropZone;
    public int handSize;

    Character playerUnit;

    List<Card> deck = new List<Card>();
    List<GameObject> activeCards = new List<GameObject>();



    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        playerUnit = character.GetComponent<Character>();
        playerUnit.namePlate.text = playerUnit.characterName;
        playerUnit.portrait.sprite = playerUnit.art;
        Draw(handSize, playerUnit.deck);
    }


    public void Execute()
    {
        foreach(Transform child in dropZone.transform)
        {
            Card playedCard = child.gameObject.GetComponent<CardDisplay>().card;
            //card functions and behaviors can be implemented here
            Debug.Log(playedCard.cardName);
        }

        //Discards current hand before drawing a new one
        foreach (GameObject card in activeCards)
        {
            Destroy(card);
        }

        Draw(handSize, playerUnit.deck);
    }

    public void Draw(int numCards, List<Card> activeDeck)
    {
        for (var i = 0; i < numCards; i++)
        {
            GameObject newCard = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
            newCard.GetComponent<CardDisplay>().card = activeDeck[Random.Range(0, activeDeck.Count)];
            newCard.transform.SetParent(playerArea.transform, false);
            activeCards.Add(newCard);
        }
    }
}
