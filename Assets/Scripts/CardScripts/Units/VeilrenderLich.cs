using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeilrenderLich : CardScript
{
    public override void InPlaySetUp()
    {
        //UnitController unit = GetComponent<UnitController>();
        //OnEnterPlay onEnter = new OnEnterPlay(unit);
        //conditions.Add(onEnter);
        //OnTurnStart onStart = new OnTurnStart(unit);
        //conditions.Add(onStart);

        //SequenceSystem.OpponentDiscard discard = new SequenceSystem.OpponentDiscard(unit.photonView.ViewID, CardSelector.TypeFilter.All, 1);
        //SequenceSystem.ResurrectMe resurrect = new SequenceSystem.ResurrectMe(unit);

        //SequenceSystem.ConditionalWrapper conditional = new SequenceSystem.ConditionalWrapper(
        //    ()=> { return MagickManager.Instance.GetTotalLevel() > 12; });

        //conditional.AddAbility(resurrect);

        //onEnter.AddAbility(discard);
        //onStart.AddAbility(conditional);
    }
}
