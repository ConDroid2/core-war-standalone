using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoyodiShardCollector : CardScript
{
    public override void InPlaySetUp()
    {
        GetComponent<Legendary>().Initialize("Voyodi's Gambit");
        InPlayCardController card = GetComponent<InPlayCardController>();
        OnEnterPlay onEnterPlay = new OnEnterPlay(card);
        conditions.Add(onEnterPlay);

        SequenceSystem.IncreaseCurrentMagick increase = new SequenceSystem.IncreaseCurrentMagick(3, "Red");
        onEnterPlay.AddAbility(increase);

        OnIgnitedSpell onIgnitedSpell = new OnIgnitedSpell(card);
        conditions.Add(onIgnitedSpell);

        SequenceSystem.RefreshActions refresh = new SequenceSystem.RefreshActions();
        refresh.SetTarget(gameObject);

        onIgnitedSpell.AddAbility(refresh);
    }
}
