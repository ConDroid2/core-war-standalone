using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using SequenceSystem;

public class UnitController : InPlayCardController
{
    public List<SequenceSystem.GameAction> AttackStack = new List<SequenceSystem.GameAction>();
    public List<SequenceSystem.GameAction> AdvanceStack = new List<SequenceSystem.GameAction>();

    public delegate void TakeDamage(int amount);
    public TakeDamage takeDamage;

    public bool actedThisTurn = false;

    public Additions Additions;

    /** Actions **/
    public UnitAdvance unitAdvance;
    public UnitAttack unitAttack;
    public UnitMoveBack unitMoveBack;
    public SequenceSystem.GameAction unitDie;
    public UnitPlay unitPlay;
    public enum ActionState { Acted, CanAct }
    public ActionState moveState;
    public ActionState attackState;

    public Action<UnitController> OnDeath;
    public Action<ActionState> OnAttackStateChanged;
    public Action<ActionState> OnMoveStateChanged;
    public Action<string> OnAddStatus;
    public Action<UnitController> OnDealDamageTo;
    public Action<int> OnTakeDamage;
    public Action OnStrengthIncreased;
    public Action OnMaxResillienceIncreased;

    private void Awake()
    {
        base.Awake();
        Additions = GetComponent<Additions>();

        GameActionList actionList = GetComponent<GameActionList>();

        unitPlay = new UnitPlay();

        unitAdvance = new UnitAdvance(this);
        actionList.actions.Add(unitAdvance);
        AdvanceStack.Add(unitAdvance);

        unitAttack = new UnitAttack(this);
        actionList.actions.Add(unitAttack);
        AttackStack.Add(unitAttack);

        unitMoveBack = new UnitMoveBack(this);
        actionList.actions.Add(unitMoveBack);

        unitDie = new UnitDie(this);
        actionList.actions.Add(unitDie);

        destroy = unitDie;
        actionList.actions.Add(destroy);
    }

    public void SetUpCardFromName(string cardName)
    {
        cardData = new Card(cardData = new Card(cardName.ConvertToCard()));

        SetUpCardInfo();
    }

    public void SetUpCardFromJson(string cardJson)
    {
        cardData = new Card(JsonUtility.FromJson<CardJson>(cardJson));

        SetUpCardInfo();
    }

    // In the future make this require a player to be passed in
    public void SetUpCardInfo()
    {
        GetComponent<CardGraphicsController>().Setup();
        //If it's a character, get it's resilience and damage 
        if (cardData.type == CardUtilities.Type.Character)
        {
            OnResilienceChange?.Invoke(cardData.maxResilience);
            OnDamageChange?.Invoke(cardData.currentStrength);
            OnInfluenceChange?.Invoke(cardData.currentInfluence);
        }

        OnNameChange?.Invoke(cardData.name);
        moveState = ActionState.Acted;
        attackState = ActionState.Acted;
        takeDamage = DefaultTakeDamage;
        CardAbilityFactory.Instance.AddInPlayCardFunctionality(this);
    }

    public void SetAttackTarget(UnitController newTarget)
    {
        unitAttack.SetAttackTarget(newTarget);
    }

    public void DefaultTakeDamage(int damage)
    {
        int newResilience = cardData.currentResilience - (damage - cardData.armor);
        OnTakeDamage?.Invoke(damage - cardData.armor);
        transform.DOShakePosition(0.1f, strength: new Vector3(1f, 1f, 0f), vibrato: 75);

        ChangeResilience(newResilience);

        if (newResilience <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        MainSequenceManager.Instance.AddNext(unitDie);
        OnDeath?.Invoke(this);
    }

    public void Clicked(PointerEventData eventData)
    {
        if (interactable)
        {

            if (attackState == ActionState.CanAct || moveState == ActionState.CanAct)
            {
                MouseManager.Instance.SetSelected(gameObject);
            }

            OnSelected?.Invoke(this);
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            InvokeOnInspected();
        }
    }

    public void ChangeResilience(int newResilience)
    {
        cardData.currentResilience = newResilience;
        OnResilienceChange?.Invoke(cardData.currentResilience);
    }

    public void IncreaseMaxResilience(int amount)
    {
        cardData.maxResilience = cardData.maxResilience + amount;
        if (cardData.maxResilience < 0)
        {
            cardData.maxResilience = 0;
        }
        if (amount > 0)
        {
            OnMaxResillienceIncreased?.Invoke();
        }
        ChangeResilience(cardData.currentResilience + amount);
    }

    public void ChangeDamage(int newDamage)
    {
        if (newDamage < 0)
        {
            newDamage = 0;
        }
        if (newDamage > 0)
        {
            OnStrengthIncreased?.Invoke();
        }
        cardData.currentStrength = newDamage;
        OnDamageChange?.Invoke(cardData.currentStrength);
    }

    public void IncreaseDamage(int amount)
    {
        ChangeDamage(cardData.currentStrength + amount);
    }

    public void ChangeInfluence(int amount)
    {
        cardData.currentInfluence = amount;
        OnInfluenceChange?.Invoke(cardData.currentInfluence);
    }

    public void AddKeyword(string keyword)
    {
        CardAbilityFactory.Instance.AddKeyword(keyword, this);
        cardData.keywords.Add(keyword);
    }

    public void RemoveKeyword(string keyword)
    {
        if (cardData.keywords.Contains(keyword))
        {
            Debug.Log("Removing keyword");
            CardAbilityFactory.Instance.RemoveKeyword(keyword, this);
        }
    }

    public void AddStatus(string statusType, int statusAmount)
    {
        if (statusAmount > 0)
        {
            if (!cardData.statusImmunities.Contains(statusType))
            {
                StatusFactory.Instance.AddStatus(statusType, statusAmount, this);
            }

            OnAddStatus?.Invoke(statusType);
        }
    }

    public void RemoveStatus(string statusType)
    {
        StatusFactory.Instance.RemoveStatus(statusType, this);
    }

    public void ChangeArmor(int changeAmount)
    {
        cardData.armor += changeAmount;
    }

    public void SetAttackActionState(ActionState newState)
    {
        if (newState == ActionState.Acted) { actedThisTurn = true; }
        attackState = newState;
        OnAttackStateChanged?.Invoke(attackState);
    }

    public void SetMoveActionState(ActionState newState)
    {
        if (newState == ActionState.Acted) { actedThisTurn = true; }
        moveState = newState;
        OnMoveStateChanged?.Invoke(moveState);
    }

    public void SetHoverActive(bool active)
    {
        if (!interactable) { return; }

        if ((attackState == ActionState.CanAct || moveState == ActionState.CanAct) && isMine && Player.Instance.myTurn)
        {
            activeHover.SetActive(active);
        }
        else if (!Player.Instance.myTurn || ((attackState == ActionState.Acted && moveState == ActionState.Acted) && isMine))
        {
            inactiveHover.SetActive(active);
        }
        else if (!isMine)
        {
            enemyHover.SetActive(active);
        }
    }
}
