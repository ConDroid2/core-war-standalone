using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmphthistAvenger : CardScript
{
    public override void InPlaySetUp()
    {
        OnMyUnitDeath onMyUnitDeath = new OnMyUnitDeath(GetComponent<InPlayCardController>().photonView.IsMine);
        conditions.Add(onMyUnitDeath);

        SequenceSystem.DrawCards draw = new SequenceSystem.DrawCards(1);

        onMyUnitDeath.AddAbility(draw);
    }
}
