using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Characters now instatiate at the start of the script, need to edit script to manage the three decks separately.
public enum BattleState { START, PLAYERTURN, ENEMYTURN, ESCAPE, VICTORY, DEFEAT, RUN }

public class BattleManager : MonoBehaviour
{
    public BattleState state;

    //Character Variables
    public GameObject characterTemplate;
    public List<PlayerCharacter> party = new List<PlayerCharacter>();

    //place where characters live
    public GameObject characterArea;

    //Card Variables
    public int handSize;
    public GameObject cardTemplate;
    List<Card> deck = new List<Card>();
    //the cards the player can see and interact with, GameObject because the cards are already attached to the card Template
    List<GameObject> activeCards = new List<GameObject>();
    //Cards that have already been played but may need to be shuffled back in
    List<Card> discardPile = new List<Card>();

    //Areas where cards can live
    public GameObject playerArea;
    public GameObject deckArea;
    public GameObject dropZone;
    public GameObject discardZone;




    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    //instatiates card templates and applys card data to them, marking each card as "owned" by a particular character so we can access their specific discard pile
    void generateCards()
    {
        foreach(PlayerCharacter partyMember in party)
        {
            foreach (Card card in partyMember.deckData)
            {
                GameObject newCard = Instantiate(cardTemplate, new Vector3(0, 0, 0), Quaternion.identity);
                newCard.GetComponent<CardDisplay>().card = card;
                newCard.GetComponent<CardDisplay>().owner = partyMember;
                newCard.transform.SetParent(deckArea.transform, false);
                partyMember.deck.Add(newCard);
            }
        }

    }

    void SetupBattle()
    {
        foreach(PlayerCharacter partyMember in party)
        {
            //clears discardpile and deck in case there is leftover data before setting up this instance of the characters
            partyMember.discardPile.Clear();
            partyMember.deck.Clear();

            GameObject newCharacter = Instantiate(characterTemplate, new Vector3(0, 0, 0), Quaternion.identity);
            newCharacter.GetComponent<CharacterDisplay>().character = partyMember;
            newCharacter.transform.SetParent(characterArea.transform, false);
        }

        generateCards();

        foreach (PlayerCharacter partyMember in party)
        {
            Draw(partyMember, handSize / party.Count, partyMember.deck);
        }
    }


    public void Execute()
    {
        foreach(Transform child in dropZone.transform)
        {
            Card playedCard = child.gameObject.GetComponent<CardDisplay>().card;
            //card functions and behaviors can be implemented here
            //Debug.Log(playedCard.cardName);
        }

        //Discards current hand before drawing a new one
        foreach (GameObject card in activeCards)
        {
            card.GetComponent<CardDisplay>().owner.discardPile.Add(card);
            //cards are moved off screen because we still want to access them but hide them from the player
            card.transform.SetParent(discardZone.transform, false);
        }
        //reset active cards to recieve a new hand
        activeCards.Clear();

        foreach(PlayerCharacter partyMember in party)
        {
            Draw(partyMember, handSize / party.Count, partyMember.deck);
        }
        
    }

    public void Draw(PlayerCharacter partyMember, int numCards, List<GameObject> activeDeck)
    {
        for (var i = 0; i < numCards; i++)
        {
            //check if deck is empty, if so shuffle
            if (activeDeck.Count <= 0)
            {
                shuffle(activeDeck, partyMember.discardPile);
            }

            int randNum = Random.Range(0, activeDeck.Count);
            activeDeck[randNum].transform.SetParent(playerArea.transform, false);
            activeCards.Add(activeDeck[randNum]);
            activeDeck.RemoveAt(randNum);
        }
    }

    //shuffle currently broken because drawing currently attaches the scriptable object to the card template prefab, need to do that in setup instead of draw, that way we have a list of GameObjects that draw pulls from instead of cards, because discard is a list of Gameobjects.  
    public void shuffle(List<GameObject> emptyDeck, List<GameObject> fullDiscard)
    {
        emptyDeck.AddRange(fullDiscard);
        fullDiscard.Clear();
    }
}
