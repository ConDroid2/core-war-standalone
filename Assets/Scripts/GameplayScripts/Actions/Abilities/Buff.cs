using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : TargetedAbility
{
    public int damageIncrease;
    public int resilienceIncrease;

    private UnitController card;

    private void Awake()
    {
        SetTarget(gameObject);
    }

    public void Initialize(int damage, int resilience)
    {
        damageIncrease = damage;
        resilienceIncrease = resilience;
    }

    public override IEnumerator ActionCoroutine()
    {
        card = target.GetComponent<UnitController>();

        card.IncreaseMaxResilience(resilienceIncrease);
        card.ChangeDamage(card.cardData.currentStrength + damageIncrease);


        OnEnd();
        return null;
    }

    public void InitiateStatus() 
    {
        MainSequenceManager.Instance.Add(this);
    }
}
