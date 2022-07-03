using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Konos : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        GetComponent<Legendary>().Initialize("Rebuke");

        OnEnterPlay onEnterPlay = new OnEnterPlay(unit);
        conditions.Add(onEnterPlay);

        SequenceSystem.ChangeCost increase = new SequenceSystem.ChangeCost(school: "Neutral", changeBy: 2);
        SequenceSystem.ChangeCost decrease = new SequenceSystem.ChangeCost(school: "Neutral", changeBy: -2);

        SequenceSystem.Aura aura = new SequenceSystem.Aura(
            increase,
            decrease,
            unit.photonView.IsMine,
            handFilter: CardSelector.HandFilter.EnemyHand,
            controller: unit);

        onEnterPlay.AddAbility(aura);
    }
}
