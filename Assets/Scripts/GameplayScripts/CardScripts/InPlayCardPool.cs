﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InPlayCardPool : MonoBehaviour
{
    [SerializeField] private InPlayCardController cardPrefab;

    private Queue<UnitController> cards = new Queue<UnitController>();
    public static InPlayCardPool Instance { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public UnitController Get() 
    {
        if(cards.Count == 0)
        {
            AddCards(1);
        }

        return cards.Dequeue();
    }

    // A version of Get that puts the card at a specific spot
    public UnitController Get(Vector3 pos) 
    {
        if(cards.Count == 0)
        {
            AddCards(1);
        }

        UnitController card = cards.Dequeue();
        card.transform.position = pos;
        card.initialPos = pos;
        

        return card;
    }

    private void AddCards(int amount) 
    {
        for (int i = 0; i < amount; i++)
        {
            UnitController newCard = PhotonNetwork.Instantiate("Prefabs/Unit", Vector3.zero, Quaternion.identity).GetComponent<UnitController>();
            newCard.gameObject.SetActive(false);
            cards.Enqueue(newCard);
        }
    }

    public void ReturnToPool(InPlayCardController card) 
    {
        card.gameObject.SetActive(false);
        // card.ClearCardInfo();

        Ability[] abilities = card.GetComponents<Ability>();
        AbilityCondition[] conditions = card.GetComponents<AbilityCondition>();
        foreach (Ability ability in abilities)
        {
            Destroy(ability);
        }
        foreach (AbilityCondition condition in conditions)
        {
            condition.Delete();
        }

        // cards.Enqueue(card);
    }
}
