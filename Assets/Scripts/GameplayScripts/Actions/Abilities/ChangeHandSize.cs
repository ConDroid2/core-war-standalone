using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHandSize : Ability
{
    int changeAmount = 0;

    public void Initialize(int changeAmount)
    {
        this.changeAmount = changeAmount;
    }

    public override IEnumerator ActionCoroutine()
    {
        Player.Instance.handSize += changeAmount;
        OnEnd();
        return null;
    }
}
