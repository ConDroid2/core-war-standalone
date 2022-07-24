using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SequenceSystem;
using System;

public class MainSequenceManager : MonoBehaviour
{
    public ActionSequencer mainSequence = new ActionSequencer();

    private bool sequenceIsRunning = false;

    public static MainSequenceManager Instance;

    public void SetUp() 
    {
        if(Instance == null)
        {
            Instance = this;
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
        //object[] rpcData = { typeName, parameters };
        //photonView.RPC("AddActionToSequence", RpcTarget.Others, rpcData);
    }

    public void InterruptCurrentAction()
    {
        if(mainSequence.currentAction != null)
            mainSequence.currentAction.Interrupt();
    }

    public void AddActionToSequence(string typeName, object[] parameters)
    {
        object action = Activator.CreateInstance(Type.GetType(typeName), parameters);
        Add(action as SequenceSystem.GameAction);
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
}
