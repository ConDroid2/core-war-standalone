using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SequenceSystem;

namespace SequenceSystem
{
    public class PlayerGainLevel : GameAction
    {
        private int amount = 0;
        private Player player;
        private GameObject energyCanvas;

        public PlayerGainLevel(Player player, int amount, GameObject energyCanvas)
        {
            this.player = player;
            this.amount = amount;
            this.energyCanvas = energyCanvas;
        }

        public PlayerGainLevel(PlayerGainLevel template)
        {
            player = template.player;
            amount = template.amount;
            energyCanvas = template.energyCanvas;
        }

        public override void PerformGameAction()
        {
            if (MagickManager.Instance.level["Blue"] < EnergyUtilities.maxLevel || MagickManager.Instance.level["Red"] < EnergyUtilities.maxLevel || MagickManager.Instance.level["Green"] < EnergyUtilities.maxLevel || MagickManager.Instance.level["Black"] < EnergyUtilities.maxLevel)
            {
                MagickManager.Instance.OnLevelChange += HandleLevelIncreased;
                energyCanvas.SetActive(true);
                MultiUseButton.Instance.SetInteractable(false);
                player.currentMode = Player.Mode.SelectEnergy;
                MagickManager.Instance.ChangeMode(MagickManager.Mode.IncreaseLevel);
            }
            else
            {
                player.currentMode = Player.Mode.Normal;
                OnEnd();
            }          
        }

        public void HandleLevelIncreased(string color, int amount)
        {
            energyCanvas.SetActive(false);
            MultiUseButton.Instance.SetInteractable(player.myTurn);
            MagickManager.Instance.OnLevelChange -= HandleLevelIncreased;
            player.currentMode = Player.Mode.Normal;
            OnEnd();
        }
    }
}
