using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AddCardToCurrentZone : GameAction, NetworkedAction
{
    InPlayCardController card;
    private void Awake()
    {
        card = GetComponent<InPlayCardController>();
    }
    public override IEnumerator ActionCoroutine()
    {
        if(card.currentZoneNum > 0)
        {
            Vector3 newPos = Vector3.zero;
            card.interactable = false;
            if (card.photonView.IsMine)
            {
                newPos = Player.Instance.zones[card.currentZoneNum - 1].AddCard(card);
                card.currentZone = Player.Instance.zones[card.currentZoneNum - 1];
            }
            else
            {
                newPos = Enemy.Instance.zones[card.currentZoneNum - 1].AddCard(card);
                card.currentZone = Enemy.Instance.zones[card.currentZoneNum - 1];
            }

            transform.DOMoveZ(transform.position.z - 4, 0.3f).OnComplete(() =>
            {
                transform.DOMove(newPos, 0.2f).OnComplete(OnEnd);
                card.interactable = true;
            });

        }

        return null;
    }
}
