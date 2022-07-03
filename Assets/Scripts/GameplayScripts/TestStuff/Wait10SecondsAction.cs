using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wait10SecondsAction : GameAction
{
    public override void PerformGameAction() 
    {
        StartCoroutine(DoAction());
    }

    IEnumerator DoAction() 
    {
        Debug.Log("Start Action Base");
        yield return new WaitForSeconds(5f);
        Debug.Log("EndActionBase");
        done = true;
    }
}
