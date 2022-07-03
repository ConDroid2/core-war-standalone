using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCardPool : MonoBehaviour
{
    [SerializeField] private EnemyCard cardPrefab;

    private Queue<EnemyCard> cards = new Queue<EnemyCard>();
    public static EnemyCardPool Instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public EnemyCard Get() 
    {
        if (cards.Count == 0)
        {
            AddCards(1);
        }

        return cards.Dequeue();
    }

    private void AddCards(int amount) 
    {
        for (int i = 0; i < amount; i++)
        {
            EnemyCard newCard = Instantiate(cardPrefab);
            newCard.gameObject.SetActive(false);
            cards.Enqueue(newCard);
        }
    }

    public void ReturnToPool(EnemyCard card) 
    {
        card.gameObject.SetActive(false);

        cards.Enqueue(card);
    }
}
