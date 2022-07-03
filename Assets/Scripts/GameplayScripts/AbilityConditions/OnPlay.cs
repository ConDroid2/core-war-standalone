using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlay : AbilityCondition
{
    CardController card;
    public OnPlay(CardController card)
    {
        this.card = card;
        this.card.OnPlay += HandleCondition;
    }

    protected override void HandleCondition() 
    {
        //foreach(SequenceSystem.Ability ability in abilities)
        //{
        //    MainSequenceManager.Instance.Add(ability);
        //}

        MainSequenceManager.Instance.Add(abilities);
    }

    public override void Delete() 
    {
        card.OnPlay -= HandleCondition;
    }

    public override void SetUp()
    {
        card.OnPlay += HandleCondition;
    }
}
