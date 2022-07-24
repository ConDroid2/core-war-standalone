using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : TargetedAbility
{
    public override IEnumerator ActionCoroutine()
    {
        UnitController card = target.GetComponent<UnitController>();
        card.ChangeResilience(card.cardData.maxResilience);

        OnEnd();
        return null;
    }
}
