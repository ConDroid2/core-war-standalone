using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingArmordillo : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnEnterPlay onEnter = new OnEnterPlay(unit);
        conditions.Add(onEnter);

        SequenceSystem.ChangeArmor changeArmor = new SequenceSystem.ChangeArmor(1, unit);

        onEnter.AddAbility(changeArmor);
        unit.AttackStack.Clear();
    }
}
