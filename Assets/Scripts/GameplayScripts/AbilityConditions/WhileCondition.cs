using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileCondition : AbilityCondition
{
    public enum ConditionDependsOn { Units }
    public delegate bool CheckCondition();

    // Dictionary of while conditions
    private Dictionary<string, WhileConditionUtility> utilityDictionary = new Dictionary<string, WhileConditionUtility>
    {
        {"EnemyHasMoreUnits", new WhileConditionUtility(ConditionDependsOn.Units, () => Player.Instance.unitManager.UnitsControlled < Enemy.Instance.unitManager.UnitsControlled) },
        {"EnemyHasUnits", new WhileConditionUtility(ConditionDependsOn.Units, () => Enemy.Instance.unitManager.UnitsControlled > 0 ) }
    };


    InPlayCardController card;
    private bool conditionInEffect = false;
    private ConditionDependsOn dependsOn;
    private CheckCondition checkCondition;
    private SequenceSystem.GameAction whenConditionMet;
    private SequenceSystem.GameAction whenConditionNoLongerMet;


    public WhileCondition(InPlayCardController card, string conditionKey, SequenceSystem.GameAction met, SequenceSystem.GameAction noLongerMet)
    {
        this.card = card;
        WhileConditionUtility utilityInfo = utilityDictionary[conditionKey];
        this.dependsOn = utilityInfo.dependsOn;
        this.checkCondition = utilityInfo.checkCondition;
        whenConditionMet = met;
        whenConditionNoLongerMet = noLongerMet;

        SetUp();
    }  

    public override void SetUp()
    {
        if (!card.isMine) return;
        if(dependsOn == ConditionDependsOn.Units) 
        {
            Enemy.Instance.unitManager.OnControlledAmountChanged += HandleCondition;
            Player.Instance.unitManager.OnControlledAmountChanged += HandleCondition;
        }

        HandleCondition();
    }

    protected override void HandleCondition()
    {
        Debug.Log("Handling Condition");
        bool isMet = checkCondition();

        if (isMet && !conditionInEffect)
        {
            MainSequenceManager.Instance.Add(whenConditionMet);
            conditionInEffect = true;
        }
        else if (!isMet && conditionInEffect)
        {
            MainSequenceManager.Instance.Add(whenConditionNoLongerMet);
            conditionInEffect = false;
        }
    }

    public override void Delete()
    {
        if (!card.isMine) return;
        if (dependsOn == ConditionDependsOn.Units)
        {
            Enemy.Instance.unitManager.OnControlledAmountChanged -= HandleCondition;
            Player.Instance.unitManager.OnControlledAmountChanged -= HandleCondition;
        }
    }

    public struct WhileConditionUtility
    {
        public ConditionDependsOn dependsOn;
        public CheckCondition checkCondition;

        public WhileConditionUtility(ConditionDependsOn dependsOn, CheckCondition checkCondition)
        {
            this.dependsOn = dependsOn;
            this.checkCondition = checkCondition;
        }
    }
}
