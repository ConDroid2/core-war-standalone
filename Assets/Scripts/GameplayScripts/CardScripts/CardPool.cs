using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CardPool : MonoBehaviour
{
    public CardController cardPrefab;

    private Queue<CardController> cards = new Queue<CardController>();
    public static CardPool Instance { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public CardController Get() 
    {
        if(cards.Count == 0)
        {
            AddCards(1);
        }

        return cards.Dequeue();
    }

    private void AddCards(int amount) 
    {
        for (int i = 0; i < amount; i++)
        {
            CardController newCard = PhotonNetwork.Instantiate("Prefabs/NewFullSizeCard", new Vector3(30f, 0f, 0f), Quaternion.identity).GetComponent<CardController>();
            newCard.gameObject.SetActive(false);
            cards.Enqueue(newCard);
        }
    }
}
