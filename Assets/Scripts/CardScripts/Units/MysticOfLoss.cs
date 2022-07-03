using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticOfLoss : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnDeath onDeath = new OnDeath(unit);
        conditions.Add(onDeath);

        SequenceSystem.AddActionToOpponent addNightmares =
            new SequenceSystem.AddActionToOpponent(
                typeof(SequenceSystem.AddCardsToHand),
                () => { return new object[] { MagickManager.Instance.level["Black"] / 2, "Nightmare" }; });

        onDeath.AddAbility(addNightmares);
    }
}
