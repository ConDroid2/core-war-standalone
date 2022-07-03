using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostshroudMage : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnemyUnitEnteredPlay onEnemyUnitEntered = new OnEnemyUnitEnteredPlay(unit.photonView.IsMine);
        conditions.Add(onEnemyUnitEntered);

        SequenceSystem.AddStatus addStatus = new SequenceSystem.AddStatus("Rooted", 2);
        onEnemyUnitEntered.AddAbility(addStatus);
    }
}
