using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDiscard : GameAction, NetworkedAction
{
    private CardController card;

    private void Awake() 
    {
        card = GetComponent<CardController>();
    }

    public override IEnumerator ActionCoroutine()
    {
        if (card.photonView.IsMine && (card.currentState == CardController.CardState.InHand || card.currentState == CardController.CardState.InHandNoCount))
        {
            Player.Instance.hand.RemoveCard(card);
        }
        else if (!card.photonView.IsMine)
        {
            Enemy.Instance.hand.RemoveCard(card);
        }

        if(card.cardData.type == CardUtilities.Type.Spell && card.cardData.script != "")
        {
            card.GetComponent<CardScript>().OnDeath();
        }

        OnEnd();

        Destroy(gameObject);

        return null;
    }
}
