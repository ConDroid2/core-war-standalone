using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldWake : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(GetComponent<CardController>());
        conditions.Add(onPlay);

        OnTurnStart onTurnStart = new OnTurnStart();
        SequenceSystem.ChooseRandomAbility chooseRandom = new SequenceSystem.ChooseRandomAbility();
        SequenceSystem.Summon summonHill = new SequenceSystem.Summon(1, "Lumbering Hill", OngoingEffectManager.Instance.gameObject);
        SequenceSystem.Summon summonWoods = new SequenceSystem.Summon(1, "Walking Woods", OngoingEffectManager.Instance.gameObject);
        SequenceSystem.Summon summonMarsh = new SequenceSystem.Summon(1, "Creeping Marsh", OngoingEffectManager.Instance.gameObject);
        chooseRandom.AddAbility(summonHill);
        chooseRandom.AddAbility(summonWoods);
        chooseRandom.AddAbility(summonMarsh);
        onTurnStart.AddAbility(chooseRandom);

        SequenceSystem.ConditionalOngoingEffect ongoingEffect = new SequenceSystem.ConditionalOngoingEffect();
        ongoingEffect.AddCondition(onTurnStart);

        SequenceSystem.InitiateOngoingEffect initiate = new SequenceSystem.InitiateOngoingEffect(ongoingEffect);

        onPlay.AddAbility(initiate);
    }
}
