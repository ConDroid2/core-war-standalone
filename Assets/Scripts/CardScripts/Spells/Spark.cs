using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : CardScript
{
    Ignite ignite;
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        // Base ability
        Ignite ignite = new Ignite(card, 3);

        SequenceSystem.DrawCards baseDraw = new SequenceSystem.DrawCards(1);
        ignite.baseAbility.AddAbility(baseDraw);

        SequenceSystem.DrawCards ignitedDraw = new SequenceSystem.DrawCards(2);
        ignite.ignitedAbility.AddAbility(ignitedDraw);
    }
}
