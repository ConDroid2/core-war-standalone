using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoiledViper : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnemyUnitEnteredPlay onEnemyUnitEntered = new OnEnemyUnitEnteredPlay(unit.photonView.IsMine);
        conditions.Add(onEnemyUnitEntered);

        SequenceSystem.Damage damage = new SequenceSystem.Damage(new IntInput("MyStrength", unit));
        onEnemyUnitEntered.AddAbility(damage);
    }
}
