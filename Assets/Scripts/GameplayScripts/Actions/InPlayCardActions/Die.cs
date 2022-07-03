using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Die : GameAction, NetworkedAction
{
    public Action<UnitController> OnDeathEvent;
    public override IEnumerator ActionCoroutine()
    {
        InPlayCardController card = GetComponent<InPlayCardController>();

        // if (card.photonView.IsMine)
        {
            card.InvokeOnRemovedFromPlay();
        }

        transform.DOScale(Vector3.zero, 0.75f);
        Tween spin = transform.DORotate(new Vector3(0, 0, 359), 0.75f);

        yield return spin.WaitForCompletion();  

        if(card.currentZone != null)
        {
            card.currentZone.RemoveCard(card);
            card.currentZone = null;
        }

        if (card.photonView.IsMine)
        {
            object[] rpcData = { card.photonView.ViewID };
            UnderworldManager.Instance.photonView.RPC("AddToUnderworld", Photon.Pun.RpcTarget.All, rpcData);
        }

        OnDeathEvent?.Invoke(card as UnitController);

        OnEnd();
    }
}
