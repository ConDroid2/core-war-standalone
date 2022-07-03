using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawMage : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();

        OnEnterPlay onEnterPlay = new OnEnterPlay(unit);
        conditions.Add(onEnterPlay);

        SequenceSystem.ChangeCost increase = new SequenceSystem.ChangeCost("Neutral", changeBy: 1);
        SequenceSystem.ChangeCost decrease = new SequenceSystem.ChangeCost("Neutral", changeBy: -1);
        SequenceSystem.Aura aura = new SequenceSystem.Aura(
                increase,
                decrease,
                true,
                handFilter: CardSelector.HandFilter.EnemyHand,
                typeFilter: CardSelector.TypeFilter.Spell);
        SequenceSystem.AuraOngoingEffect ongoingAura = new SequenceSystem.AuraOngoingEffect(aura, SequenceSystem.OngoingEffectWrapper.WhenToEnd.TurnStart);

        SequenceSystem.ReturnTargetToHand returnTarget = new SequenceSystem.ReturnTargetToHand();

        SequenceSystem.InitiateOngoingEffect initiate = new SequenceSystem.InitiateOngoingEffect(ongoingAura);
        SequenceSystem.Target target = new SequenceSystem.Target(unit, zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Unit);
        target.AddAbility(returnTarget);

        SequenceSystem.MultipleChoiceWrapper multipleChoice = new SequenceSystem.MultipleChoiceWrapper(
            target, "Return target unit to its owner's hand",
            initiate, "Spells in your enemy’s hand cost 1N more until your next turn");

        onEnterPlay.AddAbility(multipleChoice);

    }
}
