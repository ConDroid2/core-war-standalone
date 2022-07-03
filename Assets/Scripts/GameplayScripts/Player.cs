using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using SequenceSystem;
using TMPro;
public class Player : MonoBehaviour
{
    /** Deck/Card Things **/
    [HideInInspector] public CardContainer hand;
    [HideInInspector] public int handSize = 7;
    [HideInInspector] public CardController currentCard = null;
    [SerializeField] private Deck deck = null;

    /** References for actions**/
    [Header("References For Actions")]
    public CardContainer mulliganZone;
    public GameObject energyCanvas;
    public GameObject discardNotification;
    public TextMeshProUGUI discardText;

    public UnitManager unitManager;
    public SpellManager spellManager;
    public Deck GetDeck() => deck;

    public List<Zone> zones = new List<Zone>();


    /** Energy Things **/
    [HideInInspector] public string[] energyColors = { "Blue", "Red", "Green", "Black" };

    /** Player Functionality Controllers **/
    public bool myTurn = false;
    public enum Mode { SelectEnergy, Discard, WaitForInput, Normal }
    public Mode currentMode = Mode.Normal;

    //Actions
    [SerializeField] private List<SequenceSystem.GameAction> turnStartActions = new List<SequenceSystem.GameAction>();
    public List<SequenceSystem.GameAction> turnEndActions = new List<SequenceSystem.GameAction>();
    private ActionSequencer turnEndSequencer;

    public SequenceSystem.PlayerMulligan mulligan;
    public PlayerGainLevel gainLevel;
    public PlayerEmptyMagick emptyMagick;
    public PlayerResetUnits resetUnits;
    public SequenceSystem.PlayerDraw draw;
    public PlayerResetMagick resetMagick;
    public PlayerEndTurn endTurn;
    public PlayerStartTurn startTurn;
    public SequenceSystem.PlayerPayForCard pay;
    public PlayerDeactivateUnits deactivateUnits;
    public SequenceSystem.PlayerBringZonesToCapacity bringZonesToCapacity;
    public PlayerBringHandToSize bringHandToSize;

    // public PlayerPayForCard pay;

    /** Events **/
    public event Action OnEndTurn;
    public event Action OnStartTurn;
    public static event Action<Mode> OnModeChanged;

    public static Player Instance { get; private set; }

    public void SetUp()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        pay = new SequenceSystem.PlayerPayForCard(this, energyCanvas, GameObject.Find("CardWaitZone").transform);
        gainLevel = new PlayerGainLevel(this, 1, energyCanvas);
        resetMagick = new PlayerResetMagick();
        resetUnits = new PlayerResetUnits(this);
        emptyMagick = new PlayerEmptyMagick();
        draw = new SequenceSystem.PlayerDraw(this);
        mulligan = new SequenceSystem.PlayerMulligan(this);
        endTurn = new PlayerEndTurn();
        startTurn = new PlayerStartTurn();
        deactivateUnits = new PlayerDeactivateUnits(this);
        bringZonesToCapacity = new SequenceSystem.PlayerBringZonesToCapacity(this);
        bringHandToSize = new PlayerBringHandToSize(this);

        turnStartActions.Add(startTurn);
        turnStartActions.Add(draw);
        turnStartActions.Add(gainLevel);
        turnStartActions.Add(resetMagick);
        turnStartActions.Add(resetUnits);
        

        turnEndActions.Add(emptyMagick);
        turnEndActions.Add(deactivateUnits);
        turnEndActions.Add(bringHandToSize);
        turnEndActions.Add(bringZonesToCapacity);
        turnEndActions.Add(endTurn);

        hand = GetComponent<CardContainer>();
        // CardController.OnSelected += HighlightSelectedCardCost;
        CardController.OnUnselected += HandleCardUnselected;
        hand.OnCardAdded += CheckIfCardCanBePlayed;
        MultiUseButton.Instance.BackToEndTurn();

        unitManager = new UnitManager(zones);
        spellManager = new SpellManager();
    }

    // Things that need to happen at the start of a player's turn
    public void StartTurn()
    {
        myTurn = true;
        MainSequenceManager.Instance.mainSequence.AddGameAction(turnStartActions);

        OnStartTurn?.Invoke();
    }

    // Things that need to happen at the end of a player's turn
    public void EndTurn()
    {
        // Why is this not happening on the main sequence?
        OnEndTurn?.Invoke();

        MainSequenceManager.Instance.Add(turnEndActions);     
        myTurn = false;
    }

    public void ChangeMode(Mode newMode)
    {
        currentMode = newMode;
        OnModeChanged?.Invoke(currentMode);
    }

    /** Current Card Scipts **/

    public void HandleCardUnselected()
    {
        // ResetSelectedEnergy();
    }
    public void PayForCard(CardController card)
    {
        if (card.canBePayedFor && currentMode == Mode.Normal)
        {
            pay.card = card;
            MainSequenceManager.Instance.Add(pay);
        }
        else
        {
            card.ReturnToHand();
        }
    }

    public void CheckAllCardsCanBeyPlayed()
    {
        // Check all cards to see what can be played
        foreach (CardController card in hand.cards)
        {
            CheckIfCardCanBePlayed(card);
        }
    }
    public void CheckIfCardCanBePlayed(CardParent cardParent)
    {
        CardController card = cardParent as CardController;
        // Make sure the player can pay for it
        foreach(string color in energyColors)
        {
            if(card.cardData.cost[color] > MagickManager.Instance.current[color])
            {
                card.SetCanBePayedFor(false);
                return;
            }
        }

        // Only account for neutral costs if the card doesn't have Prophecy
        if (!card.cardData.keywords.Contains("Prophecy"))
        {
            if (card.cardData.cost["Neutral"] > 0)
            {
                int totalEnergy = MagickManager.Instance.current["Blue"] + MagickManager.Instance.current["Red"] + MagickManager.Instance.current["Green"] + MagickManager.Instance.current["Black"]; ;
                if (card.cardData.GetTotalCost() > totalEnergy)
                {
                    card.SetCanBePayedFor(false);
                    return;
                }
            }
        }

        card.SetCanBePayedFor(true);
        return;
    }
}
