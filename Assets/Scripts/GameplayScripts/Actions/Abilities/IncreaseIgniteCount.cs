using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseIgniteCount : Ability
{
    int amount = 1;

    public void Initialize(int amount)
    {
        this.amount = amount;
    }

    public override IEnumerator ActionCoroutine()
    {
        IgniteManager.Instance.IncreaseIgniteCount(amount);

        OnEnd();
        return null;
    }
}
