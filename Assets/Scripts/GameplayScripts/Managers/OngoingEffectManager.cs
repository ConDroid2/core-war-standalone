using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OngoingEffectManager : MonoBehaviour
{
    private List<SequenceSystem.OngoingEffectWrapper> indefiniteEffects = new List<SequenceSystem.OngoingEffectWrapper>();
    private List<SequenceSystem.OngoingEffectWrapper> untilTurnStartEffects = new List<SequenceSystem.OngoingEffectWrapper>();
    private List<SequenceSystem.OngoingEffectWrapper> untilTurnEndEffects = new List<SequenceSystem.OngoingEffectWrapper>();


    public static OngoingEffectManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

            Player.Instance.OnEndTurn += HandleTurnEnd;
            Player.Instance.OnStartTurn += HandleTurnStart;
        }
    }

    private void OnDestroy()
    {
        indefiniteEffects.Clear();
        untilTurnEndEffects.Clear();
        untilTurnStartEffects.Clear();

        Player.Instance.OnEndTurn -= HandleTurnEnd;
        Player.Instance.OnStartTurn -= HandleTurnStart;
    }

    public void AddOngoingEffect(SequenceSystem.OngoingEffectWrapper ongoingEffect)
    {
        if(ongoingEffect.whenToEnd == SequenceSystem.OngoingEffectWrapper.WhenToEnd.Never)
        {
            indefiniteEffects.Add(ongoingEffect);
        }
        else if(ongoingEffect.whenToEnd == SequenceSystem.OngoingEffectWrapper.WhenToEnd.TurnStart)
        {
            untilTurnStartEffects.Add(ongoingEffect);
        }
        else if(ongoingEffect.whenToEnd == SequenceSystem.OngoingEffectWrapper.WhenToEnd.TurnEnd)
        {
            untilTurnEndEffects.Add(ongoingEffect);
        }

        ongoingEffect.SetUp();
    }

    public void HandleTurnStart()
    {
        if(untilTurnStartEffects.Count > 0)
        {
            foreach(SequenceSystem.OngoingEffectWrapper effect in untilTurnStartEffects)
            {
                effect.EndEffect();
            }

            untilTurnStartEffects.Clear();
        }
    }

    public void HandleTurnEnd()
    {
        if (untilTurnEndEffects.Count > 0)
        {
            foreach (SequenceSystem.OngoingEffectWrapper effect in untilTurnEndEffects)
            {
                effect.EndEffect();
            }

            untilTurnEndEffects.Clear();
        }
    }
}
