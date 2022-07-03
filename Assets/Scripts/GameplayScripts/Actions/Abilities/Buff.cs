using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : TargetedAbility
{
    public int damageIncrease;
    public int resilienceIncrease;

    private InPlayCardController card;

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

        object[] resilienceData = { resilienceIncrease };
        card.photonView.RPC("IncreaseMaxResilience", Photon.Pun.RpcTarget.All, resilienceData);

        object[] damageData = { card.cardData.currentStrength + damageIncrease };
        card.photonView.RPC("ChangeDamage", Photon.Pun.RpcTarget.All, damageData);


        OnEnd();
        return null;
    }

    public void InitiateStatus() 
    {
        MainSequenceManager.Instance.Add(this);
    }
}
