using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyOfTheDead : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        PutDeckUnitsInUnderworld putUnits = new PutDeckUnitsInUnderworld();
        onPlay.AddAbility(putUnits);
    }

    public class PutDeckUnitsInUnderworld : SequenceSystem.Ability
    {
        public PutDeckUnitsInUnderworld()
        {

        }

        public PutDeckUnitsInUnderworld(PutDeckUnitsInUnderworld template)
        {

        }

        public override void PerformGameAction()
        {
            //List<Card> units = CardSelector.GetCardsFromDeck(typeFilter: CardSelector.TypeFilter.Unit);

            //foreach(Card card in units)
            //{
            //    Deck deck = Player.Instance.GetDeck();
            //    deck.DrawSpecificCard(card);
            //    UnitController unit = InPlayCardPool.Instance.Get(deck.transform.position);
            //    object[] rpcData = { card.name };
            //    unit.photonView.RPC("SetUpCardInfo", Photon.Pun.RpcTarget.All, rpcData);
            //    unit.SetUpCardInfo()


            //    UnderworldManager.Instance.photonView.RPC("AddToUnderworld", Photon.Pun.RpcTarget.All, new object[] { unit.photonView.ViewID });
            //}

            OnEnd();
        }
    }
}
