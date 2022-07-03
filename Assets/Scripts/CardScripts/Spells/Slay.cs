public class Slay : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.Target targetAbility = new SequenceSystem.Target(card, 
            zoneFilter: CardSelector.ZoneFilter.All, 
            typeFilter: CardSelector.TypeFilter.Unit, 
            originatingCard: CardSelector.OriginatingCard.TargetedAbility);
        SequenceSystem.DestroyCard destroy = new SequenceSystem.DestroyCard();

        targetAbility.abilities.Add(destroy);
        targetAbility.optional = true;

        onPlay.AddAbility(targetAbility);
    }
}
