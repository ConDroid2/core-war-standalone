using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class PlayerBringZonesToCapacity : GameAction
    {
        Player player;
        Zone currentZone;
        public PlayerBringZonesToCapacity(Player player) : base()
        {
            this.player = player;
        }

        public PlayerBringZonesToCapacity(PlayerBringZonesToCapacity template)
        {
            player = template.player;
        }

        public override void PerformGameAction()
        {
            SetCurrentZone(player.zones[0]);
        }

        private void SetCurrentZone(Zone zone)
        {
            currentZone = zone;
            if(currentZone == null)
            {
                InPlayCardController.OnSelected -= DiscardCard;
                OnEnd();
            }
            else if (currentZone.cards.Count > currentZone.cardCapacity)
            {
                currentZone.discardNotification.SetActive(true);
                InPlayCardController.OnSelected += DiscardCard;
            }
            else
            {
                SetCurrentZone(currentZone.nextZone);
            }
        }

        public void DiscardCard(InPlayCardController card)
        {
            if (currentZone != null && card.currentZone == currentZone)
            {
                if (currentZone.cards.Count > currentZone.cardCapacity)
                {
                    InPlayDiscard discard = card.discard;
                    discard.PerformGameAction();
                    object[] rpcData = { card.gameActions.actions.IndexOf(card.discard) };
                    card.photonView.RPC("AddNetworkedActionToSequence", Photon.Pun.RpcTarget.All, rpcData);
                }

                if(currentZone.cards.Count == currentZone.cardCapacity)
                {
                    currentZone.discardNotification.SetActive(false);
                    SetCurrentZone(currentZone.nextZone);
                }
            }
        }
    }
}
