using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ascend : AbilityCondition
{
    InPlayCardController card;
    public Ascend(InPlayCardController card)
    {
        this.card = card;
        //Core.Instance.OnCorePrimed += HandleCondition;
    }

    protected override void HandleCondition()
    {
        if(Core.Instance.state == Core.PrimedState.ByMe && Core.Instance.enteredCard != card && card.isMine)
        {
            Debug.Log("Triggering Ascend");
            MainSequenceManager.Instance.Add(abilities);
        }
    }

    public override void Delete()
    {
        //Core.Instance.OnCorePrimed -= HandleCondition;
    }

    public override void SetUp()
    {
        //Core.Instance.OnCorePrimed += HandleCondition;
    }
}
