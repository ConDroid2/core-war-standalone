using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ProphecyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeLeft;
    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField] private CardParent card;
    Prophecized prophecyController;

    public void SetUp(Prophecized prophecized)
    {
        card = GetComponent<CardParent>();
        prophecyController = prophecized;
        nameText.text = prophecized.card.cardData.name;

        prophecized.OnCountChanged += SetTimeLeft;

        card.cardData = new Card();
        card.cardData = prophecized.card.cardData;
    }

    public void SetTimeLeft(int time)
    {
        timeLeft.text = time.ToString();

        if(time == 0)
        {
            prophecyController.OnCountChanged -= SetTimeLeft;
            Destroy(gameObject);
        }
    }

    public void OnClicked(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            card.InvokeOnInspected();
        }
    }
}
