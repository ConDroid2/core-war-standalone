using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class Core : Zone
{
    [SerializeField] private SequenceSystem.Win winAction;
    public enum PrimedState { Neutral, ByMe, ByEnemy };
    public PrimedState state = PrimedState.Neutral;

    public InPlayCardController enteredCard;

    private int totalInfluence = 0;
    private int winningInfluence = 10;

    [Header("Locking Stuff")]
    public bool lockedForMe = false;
    public bool lockedForOpponent = false;
    public GameObject opponentLock;
    public GameObject meLock;


    public static Core Instance { get; private set; }

    private SequenceSystem.GameAction knockbackAction;

    public Action OnCorePrimed;
    public Action<int> OnInfluenceChanged;

    protected override void Awake() 
    {
        base.Awake();

        knockbackAction = new SequenceSystem.CoreKnockback();
        winAction = new SequenceSystem.Win(true);
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void CorePrimed(bool byMe)
    {
        if (byMe)
        {
            if(state != PrimedState.ByMe)
            {
                state = PrimedState.ByMe;
            }
            else
            {
                MainSequenceManager.Instance.AddNext(winAction);
            }
        }
        else
        {
            state = PrimedState.ByEnemy;
        }

        OnCorePrimed?.Invoke();
    }

    public override Vector3 AddCard(InPlayCardController card) 
    {
        enteredCard = card;
        (card as UnitController).unitAdvance.OnActionEnd += CardEnteredCore;

        return cards.AddCard(card);
    }

    public override void RemoveCard(InPlayCardController card)
    {
        cards.RemoveCard(card);
    }

    private void CardEnteredCore()
    {
        (enteredCard as UnitController).unitAdvance.OnActionEnd -= CardEnteredCore;
        //if (enteredCard.photonView.IsMine)
        //{    
        //    MainSequenceManager.Instance.Add(knockbackAction);
        //}

        // CorePrimed(enteredCard.photonView.IsMine);

        if (enteredCard.isMine)
        {
            ChangeInfluence(enteredCard.cardData.currentInfluence, true);
            MainSequenceManager.Instance.Add(enteredCard.discard);
        }
        enteredCard = null;
    }

    //These locking functions really should be RPCs

    public void LockCore(bool lockedByMe)
    {
        if (!lockedByMe)
        {
            lockedForMe = true;
            meLock.SetActive(true);
        }
        else
        {
            lockedForOpponent = true;
            opponentLock.SetActive(true);
        }

        // Turn on visuals depending on who locked by
    }

    public void UnlockCore(bool lockedByMe)
    {
        if (!lockedByMe)
        {
            lockedForMe = false;
            meLock.SetActive(false);
        }
        else
        {
            lockedForOpponent = false;
            opponentLock.SetActive(false);
        }
    }

    public void ChangeInfluence(int amount, bool sendEvent)
    {
        totalInfluence += amount;
        if(totalInfluence >= winningInfluence)
        {
            MainSequenceManager.Instance.Add(winAction);
        }
        OnInfluenceChanged?.Invoke(totalInfluence);

        if (sendEvent)
        {
            object[] eventData = { amount };
            NetworkEventSender.Instance.SendEvent(eventData, NetworkingUtilities.eventDictionary["EnemyCardEnteredCore"]);
        }
    }
}
