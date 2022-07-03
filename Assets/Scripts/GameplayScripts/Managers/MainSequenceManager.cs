using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SequenceSystem;
using System;
using Photon.Pun;

public class MainSequenceManager : MonoBehaviour
{
    public ActionSequencer mainSequence = new ActionSequencer();

    public PhotonView photonView;

    private bool sequenceIsRunning = false;

    public static MainSequenceManager Instance;

    public void SetUp() 
    {
        if(Instance == null)
        {
            Instance = this;

            photonView = GetComponent<PhotonView>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (mainSequence.Count != 0 && !mainSequence.IsRunning)
        {
            StartCoroutine(mainSequence.RunSequence());
            sequenceIsRunning = true;
        }
        if(!mainSequence.IsRunning && sequenceIsRunning && Player.Instance.myTurn)
        {
            sequenceIsRunning = false;
            if (CheckIfNoActionsRemain())
            {
                MultiUseButton.Instance.SetColorGreen();
            }
            
        }
    }

    public void Add(SequenceSystem.GameAction action) 
    {
        mainSequence.AddGameAction(action);
    }

    public void AddNext(SequenceSystem.GameAction action)
    {
        mainSequence.AddGameActionFirst(action);
    }

    public void Add(IEnumerable<SequenceSystem.GameAction> actions) 
    {
        mainSequence.AddGameAction(actions);
    }

    public void AddActionToEnemySequence(string typeName, object[] parameters)
    {
        object[] rpcData = { typeName, parameters };
        photonView.RPC("AddActionToSequence", RpcTarget.Others, rpcData);
    }

    [PunRPC]
    public void InterruptCurrentAction()
    {
        if(mainSequence.currentAction != null)
            mainSequence.currentAction.Interrupt();
    }

    [PunRPC]
    public void AddActionToSequence(string typeName, object[] parameters)
    {
        object action = Activator.CreateInstance(Type.GetType(typeName), parameters);
        Add(action as SequenceSystem.GameAction);
    }

    [PunRPC]
    public void MakeOpponentDiscard(int photonView, int amount, CardSelector.TypeFilter typeFilter)
    {
        SequenceSystem.TargetMultiple targetMultiple = new SequenceSystem.TargetMultiple(photonView: photonView, typeFilter: typeFilter, handFilter: CardSelector.HandFilter.MyHand);
        targetMultiple.SetAmountOfTargets(amount);
        targetMultiple.SetTargetMode(SequenceSystem.TargetMultiple.TargetMode.ExactAmount);
        SequenceSystem.DiscardCard discard = new SequenceSystem.DiscardCard();
        targetMultiple.AddAbility(discard);

        SendDoneEvent sendEvent = new SendDoneEvent();
        Add(targetMultiple);
        Add(sendEvent);
    }

    public bool CheckIfNoActionsRemain()
    {
        foreach (CardController card in CardSelector.GetCards(CardSelector.HandFilter.MyHand))
        {
            if (card.canBePayedFor)
            {
                return false;
            }
        }

        foreach(UnitController unit in CardSelector.GetCards(zoneFilter: CardSelector.ZoneFilter.MyZones, typeFilter: CardSelector.TypeFilter.Unit))
        {
            if(unit.moveState == UnitController.ActionState.CanAct || unit.attackState == UnitController.ActionState.CanAct)
            {
                return false;
            }
        }

        return true;
    }

    

    // Remove these when refactor done
    public void Add(GameAction action) { }
    public void AddNext(GameAction action) { }
    public void Add(IEnumerable<GameAction> actions) { }
}
