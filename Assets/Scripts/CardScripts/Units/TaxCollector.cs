using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaxCollector : CardScript
{
    public override void InPlaySetUp()
    {
        InPlayCardController unit = GetComponent<InPlayCardController>();
        OnEnterPlay onEnterPlay = new OnEnterPlay(unit);
        conditions.Add(onEnterPlay);

        SequenceSystem.ChangeCost increaseCost = new SequenceSystem.ChangeCost("Neutral", changeBy: 1);

        SequenceSystem.ChangeCost decreaseCost = new SequenceSystem.ChangeCost("Neutral", changeBy: -1);

        SequenceSystem.Aura aura = new SequenceSystem.Aura(
            increaseCost, 
            decreaseCost,
            unit.photonView.IsMine, 
            handFilter: CardSelector.HandFilter.All,
            typeFilter: CardSelector.TypeFilter.Spell);
        OnDeathEvent += aura.RemoveAura;

        onEnterPlay.AddAbility(aura);
    }
}
