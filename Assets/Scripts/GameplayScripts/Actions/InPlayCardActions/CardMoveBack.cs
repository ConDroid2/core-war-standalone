using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardMoveBack : GameAction, NetworkedAction
{
    private InPlayCardController card;

    private void Awake()
    {
        card = GetComponent<InPlayCardController>();
    }

    public override IEnumerator ActionCoroutine()
    {
        Vector3 newPos = transform.position;
        if (card.currentZone != null) 
        {
            card.currentZone.RemoveCard(card);
            if (card.currentZone.prevZone != null)
            {
                newPos = card.currentZone.prevZone.AddCard(card);
                card.currentZone = card.currentZone.prevZone;
            }
            else
            {
                card.currentZone = null;
                int moveBackBy = 0;

                if (card.photonView.IsMine) { moveBackBy = -3; }
                else { moveBackBy = 3; }
                newPos = card.transform.position + new Vector3(0, moveBackBy, 0);

                if (card.photonView.IsMine)
                {
                    MainSequenceManager.Instance.Add(GetComponent<Die>());
                }         
            }

            card.transform.DOMove(newPos, 0.2f).OnComplete(() => {
                OnEnd();
                card.currentZoneNum--;
            });
        }
        else
        {
            OnEnd();
        }

        return null;
    }
}
