using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitManager
{ 
    public List<CardParent> controlledUnits;

    public int UnitsControlled => controlledUnits.Count;

    public Action<UnitController> OnUnitDeath;
    public Action<CardParent> OnUnitAdded;
    public Action OnControlledAmountChanged;
    public Action OnUnitMaxResIncreased;
    public Action OnUnitStrengthIncreased;

    public UnitManager(List<Zone> managedZones)
    {
        controlledUnits = new List<CardParent>();
        foreach(Zone zone in managedZones)
        {
            zone.cards.OnCardAdded += AddUnit;
        }
    }

    public void AddUnit(CardParent unit)
    {
        if (unit.cardData.type == CardUtilities.Type.Character && !controlledUnits.Contains(unit))
        {
            controlledUnits.Add(unit);
            UnitController controller = unit as UnitController;
            controller.OnDeath += InvokeOnUnitDeath;
            controller.OnRemovedFromPlayExternal += HandleRemovedFromPlay;
            controller.OnMaxResillienceIncreased += InvokeOnUnitMaxResIncreased;
            controller.OnStrengthIncreased += InvokeOnUnitStrengthIncreased;

            OnControlledAmountChanged?.Invoke();
            OnUnitAdded?.Invoke(unit);
        }
    }

    public void InvokeOnUnitDeath(UnitController unit)
    {
        OnUnitDeath?.Invoke(unit);
        unit.OnDeath -= InvokeOnUnitDeath;
    }

    public void InvokeOnUnitMaxResIncreased()
    {
        OnUnitMaxResIncreased?.Invoke();
    }

    public void InvokeOnUnitStrengthIncreased()
    {
        OnUnitStrengthIncreased?.Invoke();
    }

    public void HandleRemovedFromPlay(CardParent unit)
    {
        if (controlledUnits.Contains(unit))
        {
            UnitController controller = unit as UnitController;
            controller.OnRemovedFromPlayExternal -= HandleRemovedFromPlay;
            controller.OnMaxResillienceIncreased -= InvokeOnUnitMaxResIncreased;
            controller.OnStrengthIncreased -= InvokeOnUnitStrengthIncreased;
            controlledUnits.Remove(unit);

            OnControlledAmountChanged?.Invoke();
        }
    }
}
