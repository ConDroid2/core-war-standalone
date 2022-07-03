using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Prophecized : MonoBehaviour
{
    public int turnCount = 0;
    [HideInInspector] public CardController card;

    public Action<int> OnCountChanged;
    public Action<Prophecized> OnFulfill;

    private void Awake()
    {
        card = GetComponent<CardController>();
        card.col.enabled = false;
        card.currentState = CardController.CardState.Waiting;
        Player.Instance.hand.RemoveCard(card);

        ProphecyManager.Instance.AddProphecy(this);
    }

    public void SetCountdown(int countdown)
    {
        turnCount = countdown;
        OnCountChanged?.Invoke(turnCount);
    }

    public void Countdown()
    {
        if (turnCount > 0)
        {
            turnCount--;
            OnCountChanged?.Invoke(turnCount);

            if (turnCount == 0)
            {
                Fulfill();
            }
        }
    }

    public void Fulfill()
    {
        Debug.Log("Fulfilling prophecy");
        transform.position = ProphecyManager.Instance.transform.position;
        gameObject.SetActive(true);
        card.Play();
        OnFulfill?.Invoke(this);
    }
}
