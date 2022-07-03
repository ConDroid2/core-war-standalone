using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToInPlayCardPool : GameAction
{
    public override void PerformGameAction() 
    {
        InPlayCardPool.Instance.ReturnToPool(GetComponent<InPlayCardController>());
        done = true;
    }
}
