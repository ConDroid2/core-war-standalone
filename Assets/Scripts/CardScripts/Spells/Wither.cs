using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wither : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);

        SequenceSystem.DiscardFromTopOfDeck playerDiscard = new SequenceSystem.DiscardFromTopOfDeck(() => { return (Player.Instance.GetDeck().cards.Count + 1) / 2; });

        SequenceSystem.AddActionToOpponent enemyDiscard =
            new SequenceSystem.AddActionToOpponent(
                typeof(SequenceSystem.DiscardFromTopOfDeck),
                () => { return new object[] { (Enemy.Instance.deckSize + 1) / 2 }; });

        onPlay.AddAbility(playerDiscard);
        onPlay.AddAbility(enemyDiscard);
    }
}
