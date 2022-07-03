using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Rendering;

public class CardParent : MonoBehaviour
{
    [Header("Card Settings")]
    // public CardBaseObject cardInfo;
    [HideInInspector] public Vector3 initialPos;

    public GameActionList gameActions;
    public PotentialTarget potentialTarget;

    public Collider col;

    public SortingGroup sortingGroup;
    public int sortingLayer = 0;

    [HideInInspector] public Card cardData;

    /** Events **/
    public Action<string> OnNameChange;
    public Action<int> OnResilienceChange;
    public Action<int> OnDamageChange;
    public Action<int> OnInfluenceChange;
    public Action<Card> OnCostChange;
    public Action<string> OnDescriptionChange;
    public Action<List<string>> OnKeywordsChange;
    public Action<List<string>> OnSubtypesChange;
    public Action OnClearCard;
    public Action<CardParent> OnHovered;
    public Action<CardParent> OnUnHovered;
    // Don't think cost ever turns off now
    // public Action<bool> OnToggleCost; // True = off, False = on
    public static Action<Card> OnInspected;

    /** Conditional Events **/
    // public event Action OnDeath;
    public event Action OnPlay;

    // Start is called before the first frame update
    protected void Awake() 
    {
        col = GetComponent<Collider>();
    }

    public virtual void Move(Vector3 pos) { }
    public virtual void Play(Vector3 pos) { }

    //protected void InvokeOnDeath() 
    //{
    //    OnDeath?.Invoke();
    //}

    public void InvokeOnPlay() 
    {
        OnPlay?.Invoke();
    }

    public void InvokeOnInspected()
    {
        OnInspected?.Invoke(cardData);
    }

    [PunRPC]
    public void AddComponent(Type type) 
    {
        //Component newGameAction = gameObject.AddComponent(type);
        //gameActions.actions.Add(newGameAction as GameAction);
    }

    
}
