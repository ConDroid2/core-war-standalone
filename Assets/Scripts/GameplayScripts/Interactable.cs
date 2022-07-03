using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler
{
    public bool selectable = false;

    public UnityEvent OnHover;
    public UnityEvent OnHoverExit;
    public UnityEvent<PointerEventData> OnClick;
    public UnityEvent<PointerEventData> OnPointerPressed;

    public void OnPointerEnter(PointerEventData eventData) 
    {
        // if (MouseManager.Instance.hoveredObject == null)
        {
            MouseManager.Instance.hoveredObject = gameObject;
            OnHover.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        if (MouseManager.Instance.hoveredObject == gameObject)
        {
            MouseManager.Instance.hoveredObject = null;   
        }

        OnHoverExit.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData) 
    {
        if (selectable)
        {
            OnClick.Invoke(eventData);
            // MouseManager.Instance.SetSelected(gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData) 
    {
        if (selectable)
        {
            OnPointerPressed.Invoke(eventData);
        }
    }
}
