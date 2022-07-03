using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardGraphicsController : MonoBehaviour
{
    private bool hasBeenSetUp = false;

    // References the card needs to change text
    [Header("Card Pieces")]
    [SerializeField] private GameObject blueCostBG;
    [SerializeField] private TextMeshPro blueCostTxt;
    [SerializeField] private GameObject redCostBG;
    [SerializeField] private TextMeshPro redCostTxt;
    [SerializeField] private GameObject greenCostBG;
    [SerializeField] private TextMeshPro greenCostTxt;
    [SerializeField] private GameObject blackCostBG;
    [SerializeField] private TextMeshPro blackCostTxt;
    [SerializeField] private GameObject neutralCostBG;
    [SerializeField] private TextMeshPro neutralCostTxt;
    [SerializeField] private GameObject damageBG;
    [SerializeField] private TextMeshPro damageTxt;
    [SerializeField] private GameObject resilienceBG;
    [SerializeField] private TextMeshPro resilienceTxt;
    [SerializeField] private GameObject influenceBG;
    [SerializeField] private TextMeshPro influenceTxt;

    [SerializeField] private List<Transform> energyCostPositions = new List<Transform>();

    [SerializeField] private Color betterNumberColor;
    [SerializeField] private Color worseNumberColor;

    [Header("Sub Controllers")]
    [SerializeField] private CardBorderController borderController;
    [SerializeField] private CardElementManager elementManager;
    [SerializeField] private CardArtManager artManager;

    private CardParent controller;
    // Start listening to card events
    public void Setup() 
    {
        // Ensure this stuff only gets set up once
        if (!hasBeenSetUp)
        {
            controller = GetComponent<CardParent>();
            controller.OnResilienceChange += HandleChangeResilience;
            controller.OnDamageChange += HandleChangeDamage;
            controller.OnInfluenceChange += HandleChangeInfluence;
            controller.OnNameChange += HandleChangeName;
            controller.OnCostChange += HandleChangeCost;
            controller.OnDescriptionChange += HandleChangeDescription;
            controller.OnKeywordsChange += HandleChangeKeywords;
            controller.OnSubtypesChange += HandleChangeSubtypes;
            controller.OnClearCard += HandleClearCard;
            if (elementManager != null)
                elementManager.SetUp();
            hasBeenSetUp = true;
        }

        borderController.SetUp(controller.cardData);
        artManager.SetUp(controller.cardData);
    }

    private void HandleChangeResilience(int amount) 
    {
        if (amount < 0) amount = 0;
        resilienceTxt.text = amount.ToString();

        if (controller.cardData.type == CardUtilities.Type.Spell || controller.cardData.type == CardUtilities.Type.Support)
        {
            resilienceBG.SetActive(false);
            return;
        }
        else
        {
            resilienceBG.SetActive(true);
        }

        // Figure out what color the resilience should be
        if (amount > controller.cardData.defaultResilience)
        {
            resilienceTxt.color = betterNumberColor;
        }
        else if(amount < controller.cardData.maxResilience)
        {
            resilienceTxt.color = worseNumberColor;
        }
        else
        {
            resilienceTxt.color = Color.white;
        }
    }

    private void HandleChangeDamage(int amount) 
    {
        if (controller.cardData.type == CardUtilities.Type.Spell || controller.cardData.type == CardUtilities.Type.Support)
        {
            damageBG.SetActive(false);
            return;
        } 
        else
        {
            damageBG.SetActive(true);
        }
        damageTxt.text = amount.ToString();

        // Figure out what color the damage should be
        if (amount > controller.cardData.defaultStrength)
        {
            damageTxt.color = betterNumberColor;
        }
        else if (amount < controller.cardData.defaultStrength)
        {
            damageTxt.color = worseNumberColor;
        }
        else
        {
            damageTxt.color = Color.white;
        }
    }

    private void HandleChangeCost(Card card) 
    {
        int currentCostSlot = 0;

        if (card.cost["Neutral"] > 0)
        {
            neutralCostBG.SetActive(true);
            neutralCostTxt.text = card.cost["Neutral"].ToString();
            neutralCostBG.transform.position = energyCostPositions[currentCostSlot].position;
            currentCostSlot++;
        }
        else
        {
            neutralCostBG.SetActive(false);
        }

        if (card.cost["Blue"] > 0)
        {
            blueCostBG.SetActive(true);
            blueCostTxt.text = card.cost["Blue"].ToString();
            blueCostBG.transform.position = energyCostPositions[currentCostSlot].position;
            currentCostSlot++;
        }
        else
        {
            blueCostBG.SetActive(false);
        }

        if (card.cost["Red"] > 0)
        {
            redCostBG.SetActive(true);
            redCostTxt.text = card.cost["Red"].ToString();
            redCostBG.transform.position = energyCostPositions[currentCostSlot].position;
            currentCostSlot++;
        }
        else
        {
            redCostBG.SetActive(false);
        }

        if (card.cost["Green"] > 0)
        {
            greenCostBG.SetActive(true);
            greenCostTxt.text = card.cost["Green"].ToString();
            greenCostBG.transform.position = energyCostPositions[currentCostSlot].position;
            currentCostSlot++;
        }
        else
        {
            greenCostBG.SetActive(false);
        }

        if (card.cost["Black"] > 0)
        {
            blackCostBG.SetActive(true);
            blackCostTxt.text = card.cost["Black"].ToString();
            blackCostBG.transform.position = energyCostPositions[currentCostSlot].position;
            currentCostSlot++;
        }
        else
        {
            blackCostBG.SetActive(false);
        }

    }

    private void HandleChangeInfluence(int amount)
    {
        influenceTxt.text = amount.ToString();

        if(controller.cardData.type == CardUtilities.Type.Support || controller.cardData.type == CardUtilities.Type.Spell)
        {
            influenceBG.SetActive(false);
            return;
        }
        else
        {
            influenceBG.SetActive(true);
        }

        if(amount > controller.cardData.defaultInfluence)
        {
            influenceTxt.color = betterNumberColor;
        }
        else if(amount < controller.cardData.defaultInfluence)
        {
            influenceTxt.color = worseNumberColor;
        }
        else
        {
            influenceTxt.color = Color.white;
        }
    }

    private void HandleChangeName(string name) 
    {
        elementManager.cardName.SetRegularText(name);
        elementManager.UpdatePos();
    }

    private void HandleChangeDescription(string desc) 
    {
        elementManager.description.SetRegularText(desc);
        elementManager.UpdatePos();
    }

    private void HandleChangeKeywords(List<string> keywordList)
    {
        string keywords = "";

        for(int i = 0; i < keywordList.Count; i++)
        {
            keywords += keywordList[i];

            if(i != keywordList.Count - 1)
            {
                keywords += ", ";
            }
        }

        elementManager.keywords.SetRegularText(keywords);
        elementManager.UpdatePos();
    }

    private void HandleChangeSubtypes(List<string> subtypeList)
    {
        string subtypes = "";

        for(int i = 0; i < subtypeList.Count; i++)
        {
            if(subtypeList[i] != "All" ) subtypes += subtypeList[i];

            if(i != subtypeList.Count - 1)
            {
                subtypes += " ";
            }
        }

        elementManager.subtype.SetRegularText(subtypes);
        elementManager.UpdatePos();
    }

    private void HandleClearCard() 
    {
        blueCostBG.SetActive(false);
        redCostBG.SetActive(false);
        greenCostBG.SetActive(false);
        blackCostBG.SetActive(false);
        neutralCostBG.SetActive(false);

        controller.OnResilienceChange -= HandleChangeResilience;
        controller.OnDamageChange -= HandleChangeDamage;
        controller.OnNameChange -= HandleChangeName;
        controller.OnCostChange -= HandleChangeCost;
        controller.OnDescriptionChange -= HandleChangeDescription;
        controller.OnClearCard -= HandleClearCard;
        controller.OnInfluenceChange -= HandleChangeInfluence;
    }
}
