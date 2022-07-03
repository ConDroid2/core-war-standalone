using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PutInHand : GameAction, NetworkedAction
{
    private CardController card;

    private void Awake()
    {
        card = GetComponent<CardController>();
    }

    public override IEnumerator ActionCoroutine()
    {
        if (card.photonView.IsMine)
        {
            Player.Instance.hand.AddCard(card);
        }
        else
        {
            Enemy.Instance.hand.AddCard(card);
        }

        OnEnd();

        return null;
    }
}
