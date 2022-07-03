using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : CardScript
{
    public override void InHandSetUp()
    {
        CardController controller = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(controller);
        conditions.Add(onPlay);
        OnDrawn drawn = new OnDrawn(controller);
        conditions.Add(drawn);

        SequenceSystem.TargetRandom target = new SequenceSystem.TargetRandom(controller,
            zoneFilter: CardSelector.ZoneFilter.All,
            typeFilter: CardSelector.TypeFilter.Unit);

        SequenceSystem.Damage damage = new SequenceSystem.Damage(new IntInput("3", controller));
        target.AddAbility(damage);
        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(1);
        onPlay.AddAbility(target);
        onPlay.AddAbility(draw);

        drawn.AddAbility(controller.play);
    }
}
