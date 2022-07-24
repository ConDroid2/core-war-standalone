using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SylkanVanguard : CardScript
{

    public override void InPlaySetUp()
    {
        InPlayCardController card = GetComponent<InPlayCardController>();

        OnEnterPlay onEnterPlay = new OnEnterPlay(card);
        conditions.Add(onEnterPlay);

        SequenceSystem.Summon summon = new SequenceSystem.Summon(1, "Worker Sylkan", card.gameObject);
        onEnterPlay.AddAbility(summon);

        SequenceSystem.BuffInPlay addBuff = new SequenceSystem.BuffInPlay(1, 1);
        SequenceSystem.BuffInPlay removeBuff = new SequenceSystem.BuffInPlay(-1, -1);

        SequenceSystem.Aura aura = new SequenceSystem.Aura(
            addBuff,
            removeBuff,
            card.isMine,
            zoneFilter: CardSelector.ZoneFilter.MyZones,
            typeFilter: CardSelector.TypeFilter.Unit,
            subtypeFilter: "Sylkan");
        OnDeathEvent += aura.RemoveAura;
        onEnterPlay.AddAbility(aura);
    }
}
