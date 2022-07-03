using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightTerror : CardScript
{
    public override void InHandSetUp()
    {
        //CardController card = GetComponent<CardController>();
        //OnPlay onPlay = new OnPlay(card);
        //conditions.Add(onPlay);

        //SequenceSystem.ConditionalWrapper conditional = new SequenceSystem.ConditionalWrapper(
        //    () => CardSelector.GetCards(CardSelector.HandFilter.EnemyHand, typeFilter: CardSelector.TypeFilter.Spell).Count > 0);

        //SequenceSystem.OpponentDiscard discard = new SequenceSystem.OpponentDiscard(card.photonView.ViewID, CardSelector.TypeFilter.Spell, 1);
        //SequenceSystem.AddActionToOpponent addNightmare =
        //    new SequenceSystem.AddActionToOpponent(
        //        typeof(SequenceSystem.AddCardsToHand), 
        //        new object[] { 2, "Nightmare" });

        //conditional.AddAbility(discard);
        //conditional.AddElseAbility(addNightmare);
        //onPlay.AddAbility(conditional);
    }
}
