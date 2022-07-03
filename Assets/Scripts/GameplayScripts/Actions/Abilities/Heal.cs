using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : TargetedAbility
{
    public override IEnumerator ActionCoroutine()
    {
        UnitController card = target.GetComponent<UnitController>();
        object[] rpcData = { card.cardData.maxResilience };
        card.photonView.RPC("ChangeResilience", Photon.Pun.RpcTarget.All, rpcData);
        card.ChangeResilience(card.cardData.maxResilience);

        OnEnd();
        return null;
    }
}
