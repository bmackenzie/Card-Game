using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject card;
    public GameObject playerArea;
    public GameObject enemyArea;
    public GameObject dropZone;
    public int handSize;

    public List<Card> deck = new List<Card>();
    List<GameObject> activeCards = new List<GameObject>();

    void Start()
    {

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

        Draw(handSize);
    }

    public void Draw(int numCards)
    {


        for (var i = 0; i < numCards; i++)
        {
            GameObject newCard = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
            newCard.GetComponent<CardDisplay>().card = deck[Random.Range(0, deck.Count - 1)];
            newCard.transform.SetParent(playerArea.transform, false);
            activeCards.Add(newCard);
        }
    }
}
