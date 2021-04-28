using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Characters now instatiate at the start of the script, need to edit script to manage the three decks separately.
public enum BattleState { START, PLAYERTURN, ENEMYTURN, ESCAPE, VICTORY, DEFEAT, RUN }

public class BattleManager : MonoBehaviour
{
    public BattleState state;

    
    
    public GameObject character;

    

    //Character Variables
    public GameObject characterTemplate;
    public List<PlayerCharacter> party = new List<PlayerCharacter>();
    Character playerUnit;

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
    public GameObject dropZone;
    public GameObject discardZone;




    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        foreach(PlayerCharacter partyMember in party)
        {
            GameObject newCharacter = Instantiate(characterTemplate, new Vector3(0, 0, 0), Quaternion.identity);
            newCharacter.GetComponent<CharacterDisplay>().character = partyMember;
            newCharacter.transform.SetParent(characterArea.transform, false);
        }

        //instatiate party members onto character Template
        //grab the character component of our player characters so we can access cards and stats
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
            //Debug.Log(playedCard.cardName);
        }

        //Discards current hand before drawing a new one
        foreach (GameObject card in activeCards)
        {
            discardPile.Add(card.GetComponent<CardDisplay>().card);
            //cards are moved off screen because we still want to access them but hide them from the player
            card.transform.SetParent(discardZone.transform, false);
        }
        //reset active cards to recieve a new hand
        activeCards.Clear();

        Draw(handSize, playerUnit.deck);
    }

    public void Draw(int numCards, List<Card> activeDeck)
    {
        //check if deck is empty, if so shuffle
        if (activeDeck.Count <= 0)
        {
            shuffle(activeDeck, discardPile);
        }

        for (var i = 0; i < numCards; i++)
        {
            int randNum = Random.Range(0, activeDeck.Count);
            GameObject newCard = Instantiate(cardTemplate, new Vector3(0, 0, 0), Quaternion.identity);
            newCard.GetComponent<CardDisplay>().card = activeDeck[randNum];
            newCard.transform.SetParent(playerArea.transform, false);
            activeCards.Add(newCard);
            activeDeck.RemoveAt(randNum);
        }
    }

    //shuffle currently broken because drawing currently attaches the scriptable object to the card template prefab, need to do that in setup instead of draw, that way we have a list of GameObjects that draw pulls from instead of cards, because discard is a list of Gameobjects.  
    public void shuffle(List<Card> emptyDeck, List<Card> fullDiscard)
    {
        emptyDeck.AddRange(fullDiscard);
        fullDiscard.Clear();
    }
}
