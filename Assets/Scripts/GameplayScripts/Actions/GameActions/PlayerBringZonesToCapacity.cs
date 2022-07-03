using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBringZonesToCapacity : GameAction
{
    private Player player;
    private Zone currentZone;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public override IEnumerator ActionCoroutine()
    {
        foreach(Zone zone in player.zones)
        {
            currentZone = zone;
            if(zone.cards.Count > zone.cardCapacity)
            {
                zone.discardNotification.SetActive(true);
                InPlayCardController.OnSelected += DiscardCard;

                while(zone.cards.Count > zone.cardCapacity)
                {
                    yield return null;
                }

                zone.discardNotification.SetActive(false);
                InPlayCardController.OnSelected -= DiscardCard;
            }
        }

        OnEnd();
    }

    public void DiscardCard(InPlayCardController card)
    {
        if(card.currentZone == currentZone)
        {
            InPlayCardDiscard discard = card.GetComponent<InPlayCardDiscard>();
            discard.PerformGameAction();
            //object[] rpcData = { card.gameActions.actions.IndexOf(card.GetComponent<InPlayCardDiscard>()) };
            //card.photonView.RPC("AddNetworkedActionToSequence", Photon.Pun.RpcTarget.All, rpcData);
        } 
    }
}
