using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InitiateOngoingEffect : Ability
{
    private GameObject ongoingEffectManager;
    private Type ongoingEffect;

    private void Awake()
    {
        ongoingEffectManager = GameObject.Find("OngoingEffectManager");
    }

    public void Initialize(Type type)
    {
        ongoingEffect = type;
    }

    public override IEnumerator ActionCoroutine()
    {
        OngoingEffectWrapper effect = ongoingEffectManager.AddComponent(ongoingEffect) as OngoingEffectWrapper;
        effect.SetUp();
        OnEnd();
        return null;
    }
}
