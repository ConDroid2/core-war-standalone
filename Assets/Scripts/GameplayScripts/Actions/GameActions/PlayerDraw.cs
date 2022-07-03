using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDraw : GameAction
{
    public int amount;
    public Player player = null;
    private Card cardInfo = null;

    private void Awake() 
    {
        player = GetComponent<Player>();
    }

    public override IEnumerator ActionCoroutine()
    {
        if (cardInfo == null)
        {
            cardInfo = player.GetDeck().DrawCard();
        }
        if (cardInfo != null)
        {
            CardController card = CardPool.Instance.Get();

            card.gameObject.SetActive(true);
            card.cardData = new Card(cardInfo);
            card.transform.position = player.GetDeck().transform.position;
            card.initialPos = player.GetDeck().transform.position;

            object[] rpcData = { cardInfo.fileName, false };
            card.photonView.RPC("SetUpCardInfo", Photon.Pun.RpcTarget.All, rpcData);

            ActionSequencer sequence = new ActionSequencer();
            sequence.AddGameAction(card.GetComponent<PutInHand>());

            yield return StartCoroutine(sequence.RunSequence());
        }
        else
        {
            MatchManager.Instance.YouLose(true);
        }

        cardInfo = null;


        OnEnd();
    }

    public void SetSpecificCard(Card card)
    {
        cardInfo = Player.Instance.GetDeck().DrawSpecificCard(card);
    }
}
