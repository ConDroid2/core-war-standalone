using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrionBirds : CardScript
{
    public override void InHandSetUp()
    {
        CardController card = GetComponent<CardController>();
        OnPlay onPlay = new OnPlay(card);
        conditions.Add(onPlay);

        CarrionBirdsAction action = new CarrionBirdsAction(card);

        onPlay.AddAbility(action);
    }

    public class CarrionBirdsAction : SequenceSystem.Ability
    {
        CardController card;
        public CarrionBirdsAction(CardController card) 
        {
            this.card = card;
        }
        public CarrionBirdsAction(CarrionBirdsAction template) 
        {
            card = template.card;
        }
        public override void PerformGameAction()
        {
            List<UnitController> enemySouls = new List<UnitController>();

            foreach (InPlayCardController soul in UnderworldManager.Instance.souls)
            {
                if (!soul.photonView.IsMine)
                {
                    enemySouls.Add(soul as UnitController);
                }
            }

            
            int amountToSummon = Mathf.Clamp(enemySouls.Count, 0, 5);
            SequenceSystem.Summon summon = new SequenceSystem.Summon(amountToSummon, "Raven", card.gameObject);

            foreach (UnitController soul in enemySouls)
            {
                object[] id = { soul.photonView.ViewID };
                UnderworldManager.Instance.photonView.RPC("ExorciseFromUnderworld", Photon.Pun.RpcTarget.All, id);
            }

            MainSequenceManager.Instance.Add(summon);

            OnEnd();
        }
    }
}
