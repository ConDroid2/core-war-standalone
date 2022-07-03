using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HogoFireCatcher : CardScript
{
    InPlayCardController card;
    public override void InPlaySetUp()
    {
        GetComponent<Legendary>().Initialize("Unleash Power");
        card = GetComponent<InPlayCardController>();
        if (card.photonView.IsMine)
        {
            IgniteManager.Instance.resetIgnite = false;
        }     
    }

    public override void InPlayDeath()
    {
        base.InPlayDeath();
        List<CardParent> hogos = CardSelector.GetSpecificCard(cardName: "Hogo, Fire Catcher", zoneFilter: CardSelector.ZoneFilter.MyZones);
        hogos.Remove(card);
        if(hogos.Count <= 0 && card.photonView.IsMine)
            IgniteManager.Instance.resetIgnite = true;
    }
}
