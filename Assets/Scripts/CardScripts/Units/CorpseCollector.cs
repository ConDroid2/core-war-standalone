using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseCollector : CardScript
{
    public override void InPlaySetUp()
    {
        InPlayCardController card = GetComponent<InPlayCardController>();
        OnEnterPlay onEnterPlay = new OnEnterPlay(card);
        conditions.Add(onEnterPlay);

        SequenceSystem.Damage damage = new SequenceSystem.Damage(new IntInput("3", card));

        SequenceSystem.Target target = new SequenceSystem.Target(card, zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Unit);
        target.optional = true;
        target.abilities.Add(damage);
        onEnterPlay.AddAbility(target);

        OnEnemyUnitDeath onUnitDeath = new OnEnemyUnitDeath(card.isMine);
        conditions.Add(onUnitDeath);

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(1);

        onUnitDeath.AddAbility(draw);
    }
}
