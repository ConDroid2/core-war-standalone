using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InHandCharacterPlay : GameAction
{
    private CardController card;
    private void Awake() 
    {
        card = GetComponent<CardController>();
        // card.play = this;
    }

    public override IEnumerator ActionCoroutine()
    {
        card.Discard();

        UnitController inPlayCard = InPlayCardPool.Instance.Get(transform.position);
        inPlayCard.turnPlayed = MatchManager.Instance.currentTurn;
        inPlayCard.photonView.RPC("SetUpCardInfo", RpcTarget.All, card.cardData.fileName);
        inPlayCard.gameObject.SetActive(card);  

        MainSequenceManager.Instance.Add(inPlayCard.GetComponent<CardAdvance>());

        MainSequenceManager.Instance.Add(inPlayCard.GetComponent<CharacterPlay>());

        OnEnd();
        return null;
    }
}
