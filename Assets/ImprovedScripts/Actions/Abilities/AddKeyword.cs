using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class AddKeyword : TargetedAbility
    {
        string keyword = "";
        CardParent controller;
        public AddKeyword(string keyword, CardParent controller = null)
        {
            this.keyword = keyword;
            this.controller = controller;

            if(controller != null)
             SetTarget(controller.gameObject);
        }

        public AddKeyword(AddKeyword template)
        {
            keyword = template.keyword;
            controller = template.controller;
            target = template.target;
        }

        public override void PerformGameAction()
        {
            CardParent card = target.GetComponent<CardParent>();
            if (!card.cardData.keywords.Contains(keyword))
            {
                (card as UnitController).AddKeyword(keyword);
            }

            OnEnd();
        }
    }
}
