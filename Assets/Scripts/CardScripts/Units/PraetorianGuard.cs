using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PraetorianGuard : CardScript
{
    public override void InPlaySetUp()
    {
        //UnitController card = GetComponent<UnitController>();
        //OnEnterPlay onEnterPlay = new OnEnterPlay(card);
        //conditions.Add(onEnterPlay);
        //OnRemovedFromPlay onRemoved = new OnRemovedFromPlay(card);
        //conditions.Add(onRemoved);

        //SequenceSystem.AddKeyword addResistant = new SequenceSystem.AddKeyword("Resistant");
        //SequenceSystem.AddKeyword addDefender = new SequenceSystem.AddKeyword("Defender");
        //SequenceSystem.RemoveKeyword removeResistant = new SequenceSystem.RemoveKeyword("Resistant");
        //SequenceSystem.RemoveKeyword removeDefender = new SequenceSystem.RemoveKeyword("Defender");
        //SequenceSystem.TargetSpecific targetAddRes = new SequenceSystem.TargetSpecific("Adya, God Queen", zoneFilter: CardSelector.ZoneFilter.MyZones);
        //SequenceSystem.TargetSpecific targetAddDef = new SequenceSystem.TargetSpecific("Adya, God Queen", zoneFilter: CardSelector.ZoneFilter.MyZones);
        //SequenceSystem.TargetSpecific targetRemoveRes = new SequenceSystem.TargetSpecific("Adya, God Queen", zoneFilter: CardSelector.ZoneFilter.MyZones);
        //SequenceSystem.TargetSpecific targetRemoveDef = new SequenceSystem.TargetSpecific("Adya, God Queen", zoneFilter: CardSelector.ZoneFilter.MyZones);

        //SequenceSystem.ConditionalWrapper enterConditional = new SequenceSystem.ConditionalWrapper(CheckAdyaResistant);
        //targetAddRes.AddAbility(addResistant);
        //targetAddDef.AddAbility(addDefender);
        //enterConditional.AddAbility(targetAddDef);
        //enterConditional.AddElseAbility(targetAddRes);
        //onEnterPlay.AddAbility(enterConditional);

        //SequenceSystem.ConditionalWrapper removedConditional = new SequenceSystem.ConditionalWrapper(CheckAdyaDefender);
        //targetRemoveDef.AddAbility(removeDefender);
        //targetRemoveRes.AddAbility(removeResistant);
        //removedConditional.AddAbility(targetRemoveDef);
        //removedConditional.AddElseAbility(targetRemoveRes);
        //onRemoved.AddAbility(removedConditional);

        //card.unitDie = card.returnToHand;
    }

    public bool CheckAdyaResistant()
    {
        bool check = false;
        List<CardParent> adya = CardSelector.GetSpecificCard("Adya, God Queen", CardSelector.ZoneFilter.MyZones);
        if(adya.Count > 0)
        {
            check = adya[0].cardData.keywords.Contains("Resistant");
        }

        return check;
    }

    public bool CheckAdyaDefender()
    {
        bool check = false;

        List<CardParent> adya = CardSelector.GetSpecificCard("Adya, God Queen", CardSelector.ZoneFilter.MyZones);
        if (adya.Count > 0)
        {
            check = adya[0].cardData.keywords.Contains("Defender");
        }

        return check;
    }
}
