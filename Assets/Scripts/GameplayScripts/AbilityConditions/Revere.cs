
// Triggered when player plays a card with cost 5 or greater
public class Revere : AbilityCondition
{
    InPlayCardController controller;

    public Revere(InPlayCardController controller)
    {
        this.controller = controller;
        SetUp();
    }
    public override void Delete()
    {
        if(controller.isMine)
            TriggerManager.Instance.OnCardPlayed -= HandleCondition;
    }

    public override void SetUp()
    {
        if (controller.isMine)
            TriggerManager.Instance.OnCardPlayed += HandleCondition;
    }

    private void HandleCondition(Card card)
    {
        if(card.GetTotalCost() >= 5 && card != controller.cardData)
            MainSequenceManager.Instance.Add(abilities);
    }

    protected override void HandleCondition()
    {
        throw new System.NotImplementedException();
    }
}
