using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

public class CardAdvance : GameAction, NetworkedAction
{
    UnitController card;

    private void Awake()
    {
        card = GetComponent<UnitController>();
    }

    public override IEnumerator ActionCoroutine()
    {
        Vector3 newPos = transform.position;
        card.interactable = false;
        if(card.currentZone == null)
        {
            if(card.photonView.IsMine)
            {
                newPos = Player.Instance.zones[0].AddCard(card);

                card.currentZone = Player.Instance.zones[0];
            }
            else
            {
                newPos = Enemy.Instance.zones[0].AddCard(card);

                card.currentZone = Enemy.Instance.zones[0];
            }
        }
        else if(card.currentZone.nextZone != Core.Instance || (card.photonView.IsMine && !Core.Instance.lockedForMe) || (!card.photonView.IsMine && !Core.Instance.lockedForOpponent))
        {

            card.currentZone.RemoveCard(card);
            newPos = card.currentZone.nextZone.AddCard(card);
            card.currentZone = card.currentZone.nextZone;
            card.SetAttackActionState(UnitController.ActionState.Acted);
            card.SetMoveActionState(UnitController.ActionState.Acted);
        }

        transform.DOMoveZ(transform.position.z - 4, 0.3f).OnComplete(() =>
        {
            transform.DOMove(newPos, 0.2f).OnComplete(OnEnd);
            card.interactable = true; 
            card.currentZoneNum++;
        }); 

        yield return null;
    }
}
