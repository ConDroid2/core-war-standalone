using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twincaster : CardScript
{
    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        OnNextSpell onNextSpell = new OnNextSpell(unit.photonView.IsMine);
        conditions.Add(onNextSpell);

        SequenceSystem.CreateAndPlayCard createAndPlay = new SequenceSystem.CreateAndPlayCard(
            () => { return Player.Instance.spellManager.spellsThisTurn[Player.Instance.spellManager.spellsThisTurn.Count - 1]; },
            unit.gameObject);

        onNextSpell.AddAbility(createAndPlay);
    }
}
