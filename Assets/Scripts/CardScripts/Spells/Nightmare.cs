using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightmare : CardScript
{
    public static System.Action OnDiscarded;
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        PlayInsteadOfDiscard playInstead = new PlayInsteadOfDiscard(card);
        conditions.Add(playInstead);
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);
        OnDrawn drawn = new OnDrawn(card);
        conditions.Add(drawn);

        SequenceSystem.DiscardFromTopOfDeck discard = new SequenceSystem.DiscardFromTopOfDeck(5);
        onPlay.AddAbility(discard);
        card.play = new SequenceSystem.InHandSpellPlay(card, GameObject.Find("CardWaitZone").transform);
        card.play.Interrupt();
        card.gameActions.actions.Add(card.play);

        card.discard.OnActionStart += InvokeDiscarded;

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(1);
        drawn.AddAbility(draw);
    }

    public void InvokeDiscarded()
    {
        OnDiscarded?.Invoke();
    }
}
