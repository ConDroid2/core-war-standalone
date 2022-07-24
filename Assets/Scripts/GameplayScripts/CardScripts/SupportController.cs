using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SequenceSystem;

public class SupportController : InPlayCardController
{
    public SupportDestroy supportDestroy;

    private void Awake()
    {
        base.Awake();
        GameActionList actionList = GetComponent<GameActionList>();

        supportDestroy = new SupportDestroy(this);
        actionList.actions.Add(supportDestroy);
        destroy = supportDestroy;
        actionList.actions.Add(destroy);
    }

    public override void SetUpCardFromName(string cardName)
    {
        cardData = new Card(cardData = new Card(cardName.ConvertToCard()));

        SetUpCardInfo();
    }

    public override void SetUpCardFromJson(string cardJson)
    {
        cardData = new Card(JsonUtility.FromJson<CardJson>(cardJson));

        SetUpCardInfo();
    }

    // In the future make this require a player to be passed in
    public override void SetUpCardInfo()
    {
        GetComponent<CardGraphicsController>().Setup();

        OnNameChange?.Invoke(cardData.name);
        CardAbilityFactory.Instance.AddInPlayCardFunctionality(this);
    }

    public void SetHoverActive(bool active)
    {
        if(!interactable) { return; }

        inactiveHover.SetActive(active);
    }
}
