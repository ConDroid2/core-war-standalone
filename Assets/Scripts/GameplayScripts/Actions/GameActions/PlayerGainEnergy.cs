using System.Collections;
using System;
using UnityEngine;

public class PlayerGainEnergy : GameAction
{
    public int amount = 0;
    public Player player = null;
    private bool energySelected = false;
    [SerializeField] private GameObject EnergyCanvas;

    public static event Action<bool> OnEnergyGainMode;

    private void Awake() 
    {
        player = GetComponent<Player>();
        MagickManager.Instance.OnLevelChange += HandleEnergyIncreased;
    }

    private void OnDestroy() 
    {
        MagickManager.Instance.OnLevelChange -= HandleEnergyIncreased;
    }

    public override IEnumerator ActionCoroutine() 
    {
        if (MagickManager.Instance.level["Blue"] < EnergyUtilities.maxLevel || MagickManager.Instance.level["Red"] < EnergyUtilities.maxLevel || MagickManager.Instance.level["Green"] < EnergyUtilities.maxLevel || MagickManager.Instance.level["Black"] < EnergyUtilities.maxLevel)
        {
            energySelected = false;
            EnergyCanvas.SetActive(true);
            MultiUseButton.Instance.SetInteractable(false);
            player.currentMode = Player.Mode.SelectEnergy;
            MagickManager.Instance.ChangeMode(MagickManager.Mode.IncreaseLevel);
            while (!energySelected)
            {
                yield return null;
            }

            EnergyCanvas.SetActive(false);
            MultiUseButton.Instance.SetInteractable(true);
            
        }

        OnEnergyGainMode?.Invoke(false);
        player.currentMode = Player.Mode.Normal;
        OnEnd();
    }

    // The parameters don't matter, but are needed to sub to the event
    private void HandleEnergyIncreased(string color, int amount) 
    {
        energySelected = true;
    }
}
