using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class NotificationManager : MonoBehaviour
{
    public RectTransform startingPos;
    public RectTransform middlePos;
    public RectTransform endPos;

    public TextMeshProUGUI notificationText;
    public RectTransform notification;

    public static NotificationManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        notification.gameObject.SetActive(false);
    }

    public void FireNotification(string notificationString)
    {
        notificationText.text = notificationString;
        notification.gameObject.SetActive(true);
        notification.anchoredPosition = startingPos.anchoredPosition;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(notification.DOAnchorPos(middlePos.anchoredPosition, 1f).SetEase(Ease.OutExpo));
        sequence.Append(notification.DOAnchorPos(endPos.anchoredPosition, 0.6f).SetEase(Ease.InExpo));
        sequence.OnComplete(() =>
        {
            notification.gameObject.SetActive(false);
        });
    }
}
