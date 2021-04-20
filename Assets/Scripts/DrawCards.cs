using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCards : MonoBehaviour
{
    public GameObject card;
    public GameObject playerArea;
    public GameObject enemyArea;

    public List<Card> deck = new List<Card>();
    List<GameObject> activeCards = new List<GameObject>();

    void Start()
    {

    }

    public void Draw()
    {
        //Discards current hand before drawing a new one
        foreach (GameObject card in activeCards)
        {
            Destroy(card);
        }

        for (var i = 0; i < 6; i++)
        {
            GameObject newCard = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
            newCard.GetComponent<CardDisplay>().card = deck[Random.Range(0, deck.Count - 1)];
            newCard.transform.SetParent(playerArea.transform, false);
            activeCards.Add(newCard);
        }

    }
}
