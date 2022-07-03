using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDiscardDown : GameAction
{
    private Player player;
    private int playerHand;

    [SerializeField] private GameObject notification;
    [SerializeField] private TextMeshProUGUI notificationText;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    public override IEnumerator ActionCoroutine()
    {
        if(player.hand.cards.Count > player.handSize)
        {
            notificationText.text = $"Discard down to {Player.Instance.handSize}";
            notification.SetActive(true);
            player.ChangeMode(Player.Mode.Discard);
            CardController.OnSelected += DiscardInHandCard;
            playerHand = player.hand.cards.Count;

            while (playerHand > player.handSize)
            {
                yield return null;
            }

            notification.SetActive(false);
            player.ChangeMode(Player.Mode.Normal);
            CardController.OnSelected -= DiscardInHandCard;
        }

        OnEnd();
    }

    public void DiscardInHandCard(CardController card)
    {
        playerHand--;

        CardDiscard discard = card.GetComponent<CardDiscard>();
        discard.PerformGameAction();
        //object[] rpcData = { card.gameActions.actions.IndexOf(discard) };
        //card.photonView.RPC("AddNetworkedActionToSequence", Photon.Pun.RpcTarget.Others, rpcData);
    }
}
