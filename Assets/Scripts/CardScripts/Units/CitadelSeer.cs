using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitadelSeer : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController card = GetComponent<UnitController>();

        Revere revere = new Revere(card);
        conditions.Add(revere);

        SequenceSystem.ReturnTargetToHand returnToHand = new SequenceSystem.ReturnTargetToHand();
        returnToHand.SetTarget(card.gameObject);
        revere.AddAbility(returnToHand);

        OnEnterPlay onPlay = new OnEnterPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.CountdownProphecies countdown = new SequenceSystem.CountdownProphecies(1);
        onPlay.AddAbility(countdown);
    }
}
