using System.Collections;
using System;
using UnityEngine;

public class Tutor : Keyword
{
    UnitController card;
    public TutorAction tutorAction;
    public Action<UnitController> OnTutor;
    
    private void Awake() 
    {
        card = GetComponent<UnitController>();
        if (card.photonView.IsMine)
        {
            tutorAction = new TutorAction(card);
            card.unitAdvance.OnActionStart += AddToQueue;
            card.unitMoveBack.OnActionStart += AddToQueue;
            tutorAction.OnTutor += InvokeOnTutor;
        }   
    }

    public void InvokeOnTutor(UnitController unit)
    {
        OnTutor?.Invoke(unit);
    }

    public override void RemoveKeyword()
    {
        base.RemoveKeyword();

        card.unitAdvance.OnActionStart -= AddToQueue;
        card.unitMoveBack.OnActionStart -= AddToQueue;
    }

    public class TutorAction : SequenceSystem.GameAction
    {
        UnitController card;
        public event Action<UnitController> OnTutor;
        public TutorAction(UnitController card)
        {
            this.card = card;
        }

        public TutorAction(TutorAction template) : base(template)
        {
            card = template.card;
            OnTutor = template.OnTutor;
        }

        public override void PerformGameAction()
        {
            if (card.photonView.IsMine)
            {
                if (card.currentZone != null)
                {
                    SequenceSystem.BuffInPlay buff = new SequenceSystem.BuffInPlay(1, 1);
                    foreach (InPlayCardController character in card.currentZone.cards.cards)
                    {
                        if (character.gameObject != card.gameObject && character.cardData.type == CardUtilities.Type.Character)
                        {
                            if (card.cardData.currentStrength > character.cardData.currentStrength || card.cardData.maxResilience > character.cardData.maxResilience)
                            {

                                buff.SetTarget(character.gameObject);

                                MainSequenceManager.Instance.Add(buff);

                                OnTutor?.Invoke(character as UnitController);
                            }
                        }
                    }
                }
            }

            OnEnd();
        }
    }

    public void AddToQueue() 
    {
        MainSequenceManager.Instance.AddNext(tutorAction);
    }
}
