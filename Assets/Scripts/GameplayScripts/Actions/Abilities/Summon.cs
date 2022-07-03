using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : Ability
{
    public int amount;
    public string card;

    public void Initialize(int amount, string card)
    {
        this.amount = amount;
        this.card = card;
    }

    public override IEnumerator ActionCoroutine()
    {
        for (int i = 0; i < amount; i++)
        {
            InPlayCardController cardController = InPlayCardPool.Instance.Get(transform.position);
            cardController.turnPlayed = MatchManager.Instance.currentTurn;
            object[] rpcData = { card };
            cardController.photonView.RPC("SetUpCardInfo", Photon.Pun.RpcTarget.All, rpcData);
            cardController.gameObject.SetActive(true);

            MainSequenceManager.Instance.Add(cardController.GetComponent<CardAdvance>());

            MainSequenceManager.Instance.Add(cardController.GetComponent<CharacterPlay>());
        }

        OnEnd();
        return null;
    }
}
