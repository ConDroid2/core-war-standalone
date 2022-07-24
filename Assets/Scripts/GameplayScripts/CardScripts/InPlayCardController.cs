using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;
using SequenceSystem;
[RequireComponent(typeof(ActionSequenceRunner))]
public class InPlayCardController : CardParent 
{
    [HideInInspector] public CardScript cardScript;
    [HideInInspector] public ActionSequenceRunner cardSequence;

    /** Game Actions **/
    public InPlayDiscard discard;
    public AddToCurrentZone addToCurrentZone;
    public InPlayReturnToHand returnToHand;

    [SerializeField] protected GameObject activeHover;
    [SerializeField] protected GameObject inactiveHover;
    [SerializeField] protected GameObject enemyHover;

    [HideInInspector] public int currentZoneNum = 0;
    [HideInInspector] public Zone currentZone = null;

    [HideInInspector] public int turnPlayed;
    public bool playedThisTurn => turnPlayed == MatchManager.Instance.currentTurn;

    [HideInInspector] public bool interactable = true;

    public static Action<InPlayCardController> OnSelected;
    public Action OnRemovedFromPlay;
    public Action<InPlayCardController> OnRemovedFromPlayExternal;

    public SequenceSystem.GameAction destroy;

    protected void Awake() 
    {
        base.Awake();

        GameActionList list = GetComponent<GameActionList>();
        discard = new InPlayDiscard(this);
        list.actions.Add(discard);
        addToCurrentZone = new AddToCurrentZone(this);
        list.actions.Add(addToCurrentZone);
        returnToHand = new InPlayReturnToHand(this);
        list.actions.Add(returnToHand);

        cardSequence = GetComponent<ActionSequenceRunner>();
        CardController.OnSelected += TurnOffCollider;
        CardController.OnUnselected += TurnOnCollider;
        sortingGroup = GetComponent<SortingGroup>();
    }

    private void OnDestroy() 
    {
        CardController.OnSelected -= TurnOffCollider;
        CardController.OnUnselected -= TurnOnCollider;
    }

    public virtual void SetUpCardFromName(string name) { }
    public virtual void SetUpCardFromJson(string Json) { }
    public virtual void SetUpCardInfo() { }

    /** Functional Functions **/

    public override void Move(Vector3 pos) 
    {
        initialPos = pos;
        col.enabled = false;

        transform.DOMove(initialPos, 0.1f).OnComplete(() => {
            col.enabled = true;
        });
    }

    public void TurnOffCollider(CardController card) { col.enabled = false; return; }
    public void TurnOnCollider() { col.enabled = true; return; }

    public void InvokeOnRemovedFromPlay()
    {
        OnRemovedFromPlay?.Invoke();
        OnRemovedFromPlayExternal?.Invoke(this);
    }

    public void SetCurrentZoneNum(int newCurrentZoneNum)
    {
        currentZoneNum = newCurrentZoneNum;
    }
}
