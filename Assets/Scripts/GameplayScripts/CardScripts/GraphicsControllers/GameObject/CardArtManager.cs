using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardArtManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer artImage;

    public void SetUp(Card card)
    {
        if(card.imagePath != "" && ArtLibrary.Instance.library.ContainsKey(card.imagePath))
        {
            artImage.sprite = ArtLibrary.Instance.library[card.imagePath]; 
        }
        else
        {
            artImage.sprite = ArtLibrary.Instance.defaultArt[card.primarySchool][card.type];
        }
    }
}
