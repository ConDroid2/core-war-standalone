using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.Rendering;
using SequenceSystem;

// The main script that controls the logic of the cards
public class CardController : CardParent, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Card Settings")]
    [SerializeField] private Vector3 initialScale;
    [SerializeField] private float scaleFactor;
    [SerializeField] private Transform visibleVisuals;
    [SerializeField] private Transform hiddenVisuals;
    [SerializeField] private Vector3 initialVisualPos;
    [SerializeField] private Vector3 initialVisualScale;
    public Transform visualsContainer;
    
    public enum CardState { Default, InHand, InHandNoCount, Waiting }
    public CardState currentState = CardState.Default;


    /** Controlling Variables **/
    [HideInInspector] public bool overPlayableArea = false;
    [HideInInspector] public bool canBePayedFor = false;
    private bool moving = false;
    public bool revealed = true;

    /** Card dragging variables **/
    private bool dragging = false;

    /** Standard Actions **/
    public SequenceSystem.GameAction play;
    public SequenceSystem.PutInHand putInHand;
    public SequenceSystem.GameAction discard;
    public InHandRemoveFromPlay removeFromPlay;

    /** Events **/
    public static event Action<CardController> OnSelected;
    public static event Action OnUnselected;

    /** Eventuall move this off of this class **/
    [SerializeField] private GameObject visible;
    [SerializeField] private GameObject hidden;
    [SerializeField] private GameObject inPlayAreaHighlight;
    public MeshRenderer selectable;
    public Material normalMat;
    public Material ignitedMat;
    

    private void Awake() 
    {
        base.Awake();
        GameActionList actionList = GetComponent<GameActionList>();

        putInHand = new SequenceSystem.PutInHand(this, gameObject);
        actionList.actions.Add(putInHand);
        discard = new SequenceSystem.CardDiscard(this);
        actionList.actions.Add(discard);
        removeFromPlay = new InHandRemoveFromPlay(this);
        actionList.actions.Add(removeFromPlay);

        initialVisualScale = visibleVisuals.localScale;
        initialScale = transform.localScale;
        initialPos = transform.position;
        initialVisualPos = visibleVisuals.position;
        sortingGroup = GetComponent<SortingGroup>();
    }

    public void SetUpCardFromName(string cardName, bool newSetUp = false)
    {
        cardData = new Card(cardData = new Card(cardName.ConvertToCard()));

        SetUpCardInfo(newSetUp);
    }

    public void SetUpCardFromJson(string cardJson, bool newSetUp = false)
    {
        cardData = new Card(JsonUtility.FromJson<CardJson>(cardJson));

        SetUpCardInfo(newSetUp);
    }

    // In the future make this require a player to be passed in
    private void SetUpCardInfo(bool newSetUp = false) 
    {
        GetComponent<CardGraphicsController>().Setup();

        if (cardData.type == CardUtilities.Type.Character)
        {
            play = new InHandUnitPlay(this);
        }
        else if(cardData.type == CardUtilities.Type.Spell)
        {
            play = new SequenceSystem.InHandSpellPlay(this, GameObject.Find("CardWaitZone").transform);
            gameActions.actions.Add(play);     
        }
        else if(cardData.type == CardUtilities.Type.Support)
        {
            play = new SequenceSystem.InHandSupportPlay(this);
        }

        CardAbilityFactory.Instance.AddInHandCardFunctionality(this);

        OnResilienceChange?.Invoke(cardData.currentResilience);
        OnDamageChange?.Invoke(cardData.currentStrength);
        OnInfluenceChange?.Invoke(cardData.currentInfluence);
        OnNameChange?.Invoke(cardData.name);
        OnCostChange?.Invoke(cardData);
        OnDescriptionChange?.Invoke(cardData.description);
        OnKeywordsChange?.Invoke(cardData.keywords);
        OnSubtypesChange?.Invoke(cardData.subtypes);

        if (!isMine)
        {
            visible.SetActive(false);
            hidden.SetActive(true);
            revealed = false;

            if(newSetUp)
            {
                Enemy.Instance.hand.OnCardAdded?.Invoke(this);
            }
        }
        else
        {
            if (newSetUp)
            {
                Player.Instance.hand.OnCardAdded?.Invoke(this);
            }
        }

        col.enabled = true;
        currentState = CardState.InHand;
        overPlayableArea = false;
    }

    public void ClearFunctionality()
    {

        CardScript cardScript = GetComponent<CardScript>();
        if(cardScript != null)
        {
            cardScript.InHandDeath();
            Destroy(cardScript);
        }
    }

    public void ChangeCost(string school, int newCost)
    {
        cardData.cost[school] = newCost;
        OnCostChange?.Invoke(cardData);

        Player.Instance.CheckIfCardCanBePlayed(this);
    }

    public void ChangeResilience(int changeAmount)
    {
        int newResilience = cardData.currentResilience + changeAmount;

        cardData.currentResilience = newResilience;
        cardData.maxResilience = newResilience;
        OnResilienceChange?.Invoke(cardData.currentResilience);
    }

    public void ChangeStrength(int changeAmount)
    {
        int newStrength = cardData.currentStrength + changeAmount;

        cardData.currentStrength = newStrength;
        OnDamageChange?.Invoke(cardData.currentStrength);
    }

    public void ChangeInfluence(int changeAmount)
    {
        int newInfluence = cardData.currentInfluence + changeAmount;

        cardData.currentInfluence = newInfluence;
        OnInfluenceChange?.Invoke(cardData.currentInfluence);
    }

    /** Functional Functions **/

    public void Reveal()
    {
        visible.SetActive(true);
        hidden.SetActive(false);
        revealed = true;
    }

    public virtual void Play() 
    {
        if(Player.Instance.currentCard == this)
        {
            Player.Instance.currentCard = null;
        }
        
        MainSequenceManager.Instance.Add(play);
    }

    public override void Move(Vector3 pos) 
    {
        initialPos = pos;
        initialVisualPos = pos;
        col.enabled = false;

        transform.DOMove(initialPos, 0.1f).OnComplete(()=> {
            col.enabled = true;
        });
    }

    public void ReturnToHand() 
    {
        moving = true;
        transform.DOScale(initialScale, 0.1f);
        transform.DOMove(initialPos, 0.1f).OnComplete(() =>
        {
            moving = false;
        });

        dragging = false;
        Player.Instance.currentCard = null;
        overPlayableArea = false;
        col.enabled = true;
        MagickManager.Instance.ResetSelected();
    }

    public void Discard() 
    {
        MainSequenceManager.Instance.Add(discard);
    }

    public void Remove()
    {
        MainSequenceManager.Instance.Add(removeFromPlay);
    }

    public void SetCanBePayedFor(bool canBe)
    {
        canBePayedFor = canBe;
        selectable.gameObject.SetActive(canBe);
    }

    public void SetCanBePlayed(bool flag)
    {
        overPlayableArea = flag;

        inPlayAreaHighlight.SetActive(flag);
        selectable.gameObject.SetActive(!flag);
    }

    /** Mouse Functions **/
    public void OnPointerEnter(PointerEventData eventData) 
    {
        MouseManager.Instance.hoveredObject = gameObject;

        if (!isMine) { return; }
        if (!dragging && (currentState == CardState.InHand || currentState == CardState.InHandNoCount) && Player.Instance.currentCard == null && !moving)
        {
            visibleVisuals.localScale = (initialVisualScale * scaleFactor);
            visibleVisuals.position = (initialVisualPos + new Vector3 (0f, 5f, -0.05f)); // find a way to make it not use magic numbers?
            sortingGroup.sortingOrder = 20;
            OnHovered?.Invoke(this);
        }
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        if(MouseManager.Instance.hoveredObject == gameObject)
        {
            MouseManager.Instance.hoveredObject = null;
        }

        if (!isMine) { return; }
        if (!dragging && (currentState == CardState.InHand || currentState == CardState.InHandNoCount) && !moving)
        {
            visibleVisuals.DOScale(Vector3.one, 0.1f);
            visibleVisuals.DOMove(initialVisualPos, 0.1f);
            sortingGroup.sortingOrder = sortingLayer;
            OnUnHovered?.Invoke(this);
        }
    }

    public void OnPointerDown(PointerEventData eventData) 
    {
        if (eventData.button == PointerEventData.InputButton.Left && isMine)
        {
            if (currentState == CardState.InHand)
            {
                if (Player.Instance.myTurn)
                {
                    visibleVisuals.localScale = initialVisualScale;
                    visibleVisuals.position = initialVisualPos;
                    transform.position = MouseWorldPos();
                    

                    dragging = true;

                    col.enabled = false;

                    Player.Instance.currentCard = this;
                }  
            }
            OnSelected?.Invoke(this);
        }
        else if(eventData.button == PointerEventData.InputButton.Right && revealed)
        {
            InvokeOnInspected();
        }
    }

    public void OnPointerUp(PointerEventData eventData) 
    {
        if (!isMine) { return; }
        if (overPlayableArea)
        {
            Player.Instance.PayForCard(Player.Instance.currentCard);
        }
        else
        {
            if(currentState == CardState.InHand)
            {
                ReturnToHand();
                MouseManager.Instance.ClearSelected();
            }     
        }

        OnUnselected?.Invoke();
    }

    public void OnDrag(PointerEventData eventData) 
    {
        if (!isMine || !Player.Instance.myTurn) { return; }
        if (currentState == CardState.InHand && Player.Instance.myTurn)
        {
            transform.position = MouseWorldPos();
        }
    }

    public void OnBeginDrag(PointerEventData eventData) 
    {
        if (!isMine || !Player.Instance.myTurn) { return; }
        dragging = true;
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        if (!isMine) { return; }
        dragging = false;
    }

    /** Utility Functions **/
    private Vector3 MouseWorldPos() 
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z;

        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
