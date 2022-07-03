using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : Ability
{
    public override IEnumerator ActionCoroutine()
    {
        MatchManager.Instance.YouWin(true);

        OnEnd();

        return null;
    }
}
