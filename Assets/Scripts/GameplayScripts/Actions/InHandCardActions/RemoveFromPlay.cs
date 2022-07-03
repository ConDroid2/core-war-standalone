using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the action to remove a card after it has been played
public class RemoveFromPlay : GameAction
{
    private CardController card;

    private void Awake()
    {
        card = GetComponent<CardController>();
    }

    public override IEnumerator ActionCoroutine()
    {
        if (card.photonView.IsMine && (card.currentState == CardController.CardState.InHand || card.currentState == CardController.CardState.InHandNoCount || card.currentState == CardController.CardState.Waiting))
        {
            Player.Instance.hand.RemoveCard(card);
            card.gameObject.SetActive(false);
        }
        else if (!card.photonView.IsMine)
        {
            Enemy.Instance.hand.RemoveCard(card);
            Destroy(gameObject);
        }

        OnEnd();

        return null;
    }
}
