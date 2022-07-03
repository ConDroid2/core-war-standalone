using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunScreaming : CardScript
{
    public override void InHandSetUp()
    {
        //CardController card = GetComponent<CardController>();
        //OnPlay onPlay = new OnPlay(card);
        //conditions.Add(onPlay);

        //SequenceSystem.Target target = new SequenceSystem.Target(card, zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Unit);
        //target.setActionPipeData = (thisAction) => 
        //{
        //    MainSequenceManager.Instance.mainSequence.SetPipeData(new object[] { (thisAction as SequenceSystem.Target).target.GetComponent<Photon.Pun.PhotonView>().IsMine});
        //};
        //SequenceSystem.ReturnTargetToHand returnTarget = new SequenceSystem.ReturnTargetToHand();
        //target.AddAbility(returnTarget);

        //SequenceSystem.ConditionalWrapper conditional = new SequenceSystem.ConditionalWrapper(() => { return (bool)MainSequenceManager.Instance.mainSequence.GetPipeData()[0]; });

        //SequenceSystem.AddCardsToHand addCardsToMyHand = new SequenceSystem.AddCardsToHand(1, "Nightmare");
        //SequenceSystem.AddActionToOpponent addCardsToOpponent =
        //    new SequenceSystem.AddActionToOpponent(
        //        typeof(SequenceSystem.AddCardsToHand),
        //        new object[] { 1, "Nightmare" });

        //conditional.AddAbility(addCardsToMyHand);
        //conditional.AddElseAbility(addCardsToOpponent);

        //onPlay.AddAbility(target);
        //onPlay.AddAbility(conditional);
    }
}
