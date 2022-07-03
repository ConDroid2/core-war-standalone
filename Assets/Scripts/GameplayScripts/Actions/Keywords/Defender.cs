using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defender : Keyword
{
    InPlayCardController card;
    private void Awake()
    {
        card = GetComponent<InPlayCardController>();
        Core.Instance.LockCore(card.photonView.IsMine);

        card.OnRemovedFromPlay += HandleRemovedFromPlay;
    }

    public void HandleRemovedFromPlay()
    {
        if (card.photonView.IsMine)
        {
            List<CardParent> defenders = CardSelector.GetCards(zoneFilter: CardSelector.ZoneFilter.MyZones, typeFilter: CardSelector.TypeFilter.Unit, keywordFilter: "Defender");
            if (defenders.Contains(card)) defenders.Remove(card);
            if (defenders.Count == 0) Core.Instance.UnlockCore(card.photonView.IsMine);
        }
        else
        {
            List<CardParent> defenders = CardSelector.GetCards(zoneFilter: CardSelector.ZoneFilter.EnemyZones, typeFilter: CardSelector.TypeFilter.Unit, keywordFilter: "Defender");
            if (defenders.Contains(card)) defenders.Remove(card);
            if (defenders.Count == 0) Core.Instance.UnlockCore(card.photonView.IsMine);
        }
    }

    public override void RemoveKeyword()
    {
        HandleRemovedFromPlay();
        base.RemoveKeyword(); 
    }
}
