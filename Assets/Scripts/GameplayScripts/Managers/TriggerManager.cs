using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TriggerManager : MonoBehaviour
{
    public static TriggerManager Instance { get; private set; }

    public event Action<Card> OnCardPlayed;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void InvokeOnCardPlayed(Card card)
    {
        OnCardPlayed?.Invoke(card);
    }
}
