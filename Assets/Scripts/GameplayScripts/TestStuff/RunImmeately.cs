using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunImmeately : GameAction
{
    public override void PerformGameAction() 
    {
        Debug.Log("Sub action");
        done = true;
    }
}
