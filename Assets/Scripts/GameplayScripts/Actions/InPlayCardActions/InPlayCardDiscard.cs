using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InPlayCardDiscard : GameAction, NetworkedAction
{
    public override IEnumerator ActionCoroutine()
    {
        

        InPlayCardController card = GetComponent<InPlayCardController>();

        if (card.currentZone != null)
        {
            card.currentZone.RemoveCard(card);
            card.currentZone = null;
        }

        Tween shrink = transform.DOScale(Vector3.zero, 0.5f);

        yield return shrink.WaitForCompletion();

        

        // if (card.photonView.IsMine)
        {
            card.InvokeOnRemovedFromPlay();
        }
        
        if(card.cardData.script != "")
        {
            CardScript script = card.GetComponent<CardScript>();
            script.OnDeath();
            script.OnExorcise();
        }

        OnEnd();

        Destroy(gameObject);
    }
}
