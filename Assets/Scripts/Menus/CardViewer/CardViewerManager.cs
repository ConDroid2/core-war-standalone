using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardViewerManager : MonoBehaviour
{
    public UICardGraphicsController card;
    public GameObject panel;

    private bool inspectingCard = false;

    public UnityEvent OnClosed;

    private void Awake()
    {
        card.gameObject.SetActive(false);
        panel.SetActive(false);

        CardParent.OnInspected += HandleCardInspected;
    }

    private void OnDestroy()
    {
        CardParent.OnInspected -= HandleCardInspected;
    }

    public void HandleCardInspected(Card cardInfo)
    { 
        card.gameObject.SetActive(true);
        panel.SetActive(true);

        card.FillInInfo(cardInfo);

        inspectingCard = true;
    }

    private void Update()
    {
        if (!inspectingCard) return;

        if (Input.GetMouseButtonDown(0))
        {
            inspectingCard = false;
            OnClosed?.Invoke();
        }
    }
}
