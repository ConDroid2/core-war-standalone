using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformation : Ability
{
    string transformInto = "";

    public void Initialize(string transformInto)
    {
        this.transformInto = transformInto;
    }

    public override IEnumerator ActionCoroutine()
    {
        InPlayCardController card = GetComponent<InPlayCardController>();
        // Create new unit
        InPlayCardController newCard = InPlayCardPool.Instance.Get(transform.position);
        newCard.photonView.RPC("SetUpCardInfo", Photon.Pun.RpcTarget.All, transformInto);
        newCard.gameObject.SetActive(true);
        newCard.turnPlayed = card.turnPlayed;

        // Put it in this card's zone
        newCard.SetCurrentZoneNum(card.currentZoneNum);
        ActionSequencer sequence = new ActionSequencer();
        sequence.AddGameAction(newCard.GetComponent<AddCardToCurrentZone>());
        yield return StartCoroutine(sequence.RunSequence());
        
        // Set it's action states to the same as this card's
        if(newCard.cardData.type == CardUtilities.Type.Character)
        {
            UnitController newUnit = newCard as UnitController;
            UnitController unit = card as UnitController;

            newUnit.SetAttackActionState(unit.attackState);
            newUnit.SetMoveActionState(unit.moveState);
            newUnit.actedThisTurn = unit.actedThisTurn;
        }

        MainSequenceManager.Instance.Add(GetComponent<InPlayCardDiscard>());

        OnEnd();
    }
}
