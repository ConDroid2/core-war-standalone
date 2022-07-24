using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swift : Keyword
{
    private void Awake()
    {
        if (GetComponent<InPlayCardController>().playedThisTurn)
        {
            SequenceSystem.RefreshActions refresh = new SequenceSystem.RefreshActions();
            refresh.SetTarget(gameObject);
            MainSequenceManager.Instance.Add(refresh);
        }
    }
}
