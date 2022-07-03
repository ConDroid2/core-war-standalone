using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoanShark : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        Revere revere = new Revere(unit);
        conditions.Add(revere);

        SequenceSystem.ChangeCost increase = new SequenceSystem.ChangeCost("Neutral", changeBy: 2);
        SequenceSystem.ChangeCost decrease = new SequenceSystem.ChangeCost("Neutral", changeBy: -2);
        SequenceSystem.Aura aura = new SequenceSystem.Aura(
                increase,
                decrease,
                true,
                handFilter: CardSelector.HandFilter.EnemyHand,
                typeFilter: CardSelector.TypeFilter.Spell);
        SequenceSystem.AuraOngoingEffect ongoingAura = new SequenceSystem.AuraOngoingEffect(aura, SequenceSystem.OngoingEffectWrapper.WhenToEnd.TurnStart);

        SequenceSystem.InitiateOngoingEffect initiate = new SequenceSystem.InitiateOngoingEffect(ongoingAura);
        revere.AddAbility(initiate);
    }
}
