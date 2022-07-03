using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SequenceSystem 
{
    public class PlayerBringHandToSize : GameAction
    {
        Player player;

        int playerHand;
        [SerializeField] private GameObject notification;
        [SerializeField] private TextMeshProUGUI notificationText;

        public PlayerBringHandToSize(Player player) : base()
        {
            this.player = player;
            notification = player.discardNotification;
            notificationText = player.discardText;
        }

        public PlayerBringHandToSize(PlayerBringHandToSize template)
        {
            player = template.player;
            notification = player.discardNotification;
            notificationText = player.discardText;
        }

        public override void PerformGameAction()
        {
            if (player.hand.cards.Count > player.handSize)
            {
                notificationText.text = $"Discard down to {Player.Instance.handSize}";
                notification.SetActive(true);
                player.ChangeMode(Player.Mode.Discard);
                CardController.OnSelected += DiscardInHandCard;
                playerHand = player.hand.cards.Count;
            }
            else
            {
                OnEnd();
            }
        }

        public void DiscardInHandCard(CardController card)
        {
            playerHand--;

            GameAction discard = card.discard;
            discard.PerformGameAction();
            if (card.gameActions.actions.Contains(discard))
            {
                object[] rpcData = { card.gameActions.actions.IndexOf(discard) };
                card.photonView.RPC("AddNetworkedActionToSequence", Photon.Pun.RpcTarget.Others, rpcData);
            }

            if(playerHand == player.handSize)
            {
                notification.SetActive(false);
                player.ChangeMode(Player.Mode.Normal);
                CardController.OnSelected -= DiscardInHandCard;

                OnEnd();
            }
        }
    }
}
