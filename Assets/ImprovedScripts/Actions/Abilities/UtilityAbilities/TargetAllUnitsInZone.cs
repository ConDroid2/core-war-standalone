using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SequenceSystem 
{
    public class TargetAllUnitsInZone : Ability
    {
        CardSelector.ZoneFilter zoneFilter;
        int zoneNum;
        List<TargetedAbility> abilities = new List<TargetedAbility>(); 
        public TargetAllUnitsInZone(int zoneNum, CardSelector.ZoneFilter zoneFilter)
        {
            this.zoneNum = zoneNum;
            this.zoneFilter = zoneFilter;
        }

        public TargetAllUnitsInZone(TargetAllUnitsInZone template)
        {
            zoneNum = template.zoneNum;
            zoneFilter = template.zoneFilter;
            foreach(TargetedAbility ability in template.abilities)
            {
                AddAbility(ability);
            }
        }

        public override void PerformGameAction()
        {
            Zone zone = null;

            if(zoneFilter == CardSelector.ZoneFilter.MyZones)
            {
                zone = Player.Instance.zones[zoneNum - 1];
            }
            else if(zoneFilter == CardSelector.ZoneFilter.EnemyZones)
            {
                zone = Enemy.Instance.zones[zoneNum - 1];
            }

            foreach(TargetedAbility ability in abilities)
            {
                foreach(CardParent card in zone.cards.cards)
                {
                    ability.SetTarget(card.gameObject);
                    MainSequenceManager.Instance.AddNext(ability);
                }
            }

            OnEnd();
        }

        public void AddAbility(TargetedAbility ability)
        {
            abilities.Add(ability);
        }
    }
}
