public class Maelstrom : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        SequenceSystem.TargetAll targetAll = new SequenceSystem.TargetAll(card, zoneFilter: CardSelector.ZoneFilter.All, typeFilter: CardSelector.TypeFilter.Unit);
        SequenceSystem.DestroyCard destroy = new SequenceSystem.DestroyCard();
        targetAll.AddAbility(destroy);

        onPlay.AddAbility(targetAll);
    }
}
