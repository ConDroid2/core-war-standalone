using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodlandGuide : CardScript
{
    Tutor tutor;
    SequenceSystem.AddKeyword addTutor;
    SequenceSystem.AddKeyword addResistant;

    public override void InPlaySetUp()
    {
        UnitController unit = GetComponent<UnitController>();
        if (unit.photonView.IsMine)
        {
            tutor = GetComponent<Tutor>();
            addTutor = new SequenceSystem.AddKeyword("Tutor", unit);
            addResistant = new SequenceSystem.AddKeyword("Resistant", unit);

            tutor.OnTutor += HandleTutor;
        }
    }

    public override void InPlayDeath()
    {
        base.InPlayDeath();
        tutor.tutorAction.OnTutor -= HandleTutor;
    }

    public void HandleTutor(InPlayCardController card)
    {
        addTutor.SetTarget(card.gameObject);
        addResistant.SetTarget(card.gameObject);
        card.cardSequence.Add(addTutor);
        card.cardSequence.Add(addResistant);
    }
}
