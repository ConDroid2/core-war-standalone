using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class ProphecyManager : MonoBehaviour
{
    public RectTransform prophecyPrefab;

    public GameObject prophecyPanel;
    private bool panelOpen = true;

    public List<Prophecized> prophecies = new List<Prophecized>();

    public Action OnRemoveProphecy;
    public Action OnSpellProphesied;

    public static ProphecyManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        Player.Instance.OnStartTurn += CountdownAllPropheciesOnce;
        prophecyPanel.SetActive(false);
        panelOpen = false;
    }

    private void OnDestroy()
    {
        Player.Instance.OnStartTurn -= CountdownAllPropheciesOnce;
    }

    public void OnClicked()
    {
        panelOpen = !panelOpen;
        prophecyPanel.SetActive(panelOpen);    
    }

    public void AddProphecy(Prophecized prophecy)
    {
        RectTransform newProphecy =  Instantiate(prophecyPrefab, prophecyPanel.transform);
        newProphecy.GetComponent<ProphecyUI>().SetUp(prophecy);

        prophecy.gameObject.SetActive(false);

        prophecies.Add(prophecy);
        prophecy.OnFulfill += RemoveProphecy;

        if(prophecy.card.cardData.type == CardUtilities.Type.Spell)
        {
            OnSpellProphesied?.Invoke();
        }    
    }

    public void RemoveProphecy(Prophecized prophecy)
    {
        if (prophecies.Contains(prophecy))
        {
            prophecies.Remove(prophecy);
            OnRemoveProphecy?.Invoke();
        }
    }

    public void CountdownAllPropheciesOnce()
    {
        List<Prophecized> propheciesCopy = new List<Prophecized>(prophecies);
        foreach(Prophecized prophecy in propheciesCopy)
        {
            prophecy.Countdown();
        }
    }
}
