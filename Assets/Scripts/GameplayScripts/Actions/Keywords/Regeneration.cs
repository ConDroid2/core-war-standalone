using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : Keyword
{
    private InPlayCardController card;

    private void Awake()
    {
        card = GetComponent<InPlayCardController>();

        Player.Instance.OnEndTurn += ResetResilience;
        Player.Instance.OnStartTurn += ResetResilience;
    }

    public void ResetResilience()
    {
        if(card.cardData.currentResilience < card.cardData.maxResilience)
        {
            object[] rpcData = { card.cardData.maxResilience };
            card.photonView.RPC("ChangeResilience", Photon.Pun.RpcTarget.All, rpcData);
        }
    }
}
 