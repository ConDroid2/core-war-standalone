using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherTheTribe : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.Target target = new SequenceSystem.Target(card, zoneFilter: CardSelector.ZoneFilter.MyZones, typeFilter: CardSelector.TypeFilter.Unit);
        target.setActionPipeData = (thisAction) =>
        {
            MainSequenceManager.Instance.mainSequence.SetPipeData(
            new object[] { string.Join(",", (thisAction as SequenceSystem.Target).target.GetComponent<UnitController>().cardData.subtypes) });
        };

        SequenceSystem.DrawRandomCards draw =
            new SequenceSystem.DrawRandomCards(
                amount: 3, typeFilter: CardSelector.TypeFilter.Unit, getUnique: true,
                subtypeFilter: new StringInput(() => {
                        return (string)MainSequenceManager.Instance.mainSequence.GetPipeData()[0];
                    })
                );

        onPlay.AddAbility(target);
        onPlay.AddAbility(draw);

    }
}
