using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class EnemyCard : CardParent, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject damageBG;
    [SerializeField] private TextMeshPro damageTxt;
    [SerializeField] private GameObject resilienceBG;
    [SerializeField] private TextMeshPro resilienceTxt;
    [SerializeField] private TextMeshPro nameTxt;

    [HideInInspector] public int currentZone = 0;

    //[HideInInspector] public string name;
    //[HideInInspector] public int resilience;
    //[HideInInspector] public int damage;

    //public CardBaseObject cardInfo;

    [SerializeField] private GameObject miniHover;

    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        // setUpCardInfo();
    }

    public void OnPointerEnter(PointerEventData eventData) 
    {

        MouseManager.Instance.hoveredObject = gameObject;
        miniHover.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) 
    {
        MouseManager.Instance.hoveredObject = null;
        miniHover.SetActive(false);
    }

    // Copy over all the info from the CBO
    // In the future make this require a player to be passed in
    //public void setUpCardInfo() 
    //{
    //    name = cardInfo.cardName;
    //    //If it's a character, get it's resilience and damage 
    //    if (cardInfo.type == CardUtilities.Type.Character)
    //    {
    //        resilience = cardInfo.resilience;

    //        if (resilience == 0)
    //        {
    //            resilienceBG.SetActive(false);
    //        }
    //        damage = cardInfo.damage;
    //    }

    //    setUpCardText();
    //}

    // Set up the visual info on the card
    //private void setUpCardText() 
    //{
    //    nameTxt.text = name;

    //    if (cardInfo.type == CardUtilities.Type.Character)
    //    {
    //        damageTxt.text = damage.ToString();
    //        resilienceTxt.text = resilience.ToString();
    //    }
    //}

    //public void TakeDamage(int damage, bool sendEvent) 
    //{
    //    resilience -= damage;

    //    if(resilience <= 0)
    //    {
    //        resilienceBG.SetActive(false);
    //    }
    //    resilienceTxt.text = resilience.ToString();
    //    transform.DOShakePosition(0.1f, vibrato: 75);

    //    if (sendEvent)
    //    {
    //        //object[] eventContent = { currentZone, Enemy.Instance.zones[currentZone].cards.IndexOf(this), damage };
    //        //NetworkEventSender.Instance.SendEvent(eventContent, NetworkingUtilities.eventDictionary["MyCardDamaged"]);
    //    }
    //}

    public override void Move(Vector3 pos) 
    {
        initialPos = pos;
        col.enabled = false;

        transform.DOMove(initialPos, 0.1f).OnComplete(() => {
            col.enabled = true;
        });
    }
}
