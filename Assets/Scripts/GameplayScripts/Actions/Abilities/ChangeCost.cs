using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCost : TargetedAbility
{
    private string school = "";
    private int changeBy = 0;
    private int setToAmount = -1;

    public void Initialize(string school = "", int changeBy = 0, int setToAmount = -1)
    {
        this.school = school;
        this.changeBy = changeBy;
        this.setToAmount = setToAmount;
    }

    public override IEnumerator ActionCoroutine()
    {
        CardController card = target.GetComponent<CardController>();

        if(school != "")
        {
            if(changeBy != 0)
            {
                Debug.Log("Changing " + school + " cost to " + (card.cardData.cost[school] + changeBy));
                card.ChangeCost(school, card.cardData.cost[school] + changeBy);
            }
            else if(setToAmount != 0)
            {
                card.ChangeCost(school, setToAmount);
            }
        }
        else
        {
            foreach(string color in MagickManager.Instance.costColors)
            {
                if(changeBy != 0)
                {
                    card.ChangeCost(color, card.cardData.cost[color] + changeBy);
                }
                else if(setToAmount != -1)
                {
                    card.ChangeCost(color, setToAmount);
                }
            }
        }

        OnEnd();
        return null;
    }
}
