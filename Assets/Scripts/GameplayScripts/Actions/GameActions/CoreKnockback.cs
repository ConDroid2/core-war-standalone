using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreKnockback : GameAction
{
    public override IEnumerator ActionCoroutine()
    {
        int amountOfCardsInZone = Player.Instance.zones[1].cards.cards.Count;
        int cardToPushBack = 0;
        ActionSequencer sequence = new ActionSequencer();
        while (amountOfCardsInZone > 0)
        {
            InPlayCardController inPlayCard = Player.Instance.zones[1].cards.cards[cardToPushBack].GetComponent<InPlayCardController>();
            sequence.AddGameAction(inPlayCard.GetComponent<CardMoveBack>());
            yield return StartCoroutine(sequence.RunSequence());
            // cardToPushBack++;
            amountOfCardsInZone--;
        }

        OnEnd();
    }
}
