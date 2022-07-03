using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddKeyword : TargetedAbility
{
    string keyword = "";

    private void Awake()
    {
        SetTarget(gameObject);
    }


    public override IEnumerator ActionCoroutine()
    {
        CardParent card = target.GetComponent<CardParent>();
        if(!card.cardData.keywords.Contains(keyword))
        {
            (card as UnitController).AddKeyword(keyword);
        }
       
        OnEnd();
        return null;
    }

    public void Initialize(string keyword)
    {
        this.keyword = keyword;
    }


}
