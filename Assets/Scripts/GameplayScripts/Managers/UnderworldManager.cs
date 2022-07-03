using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UnderworldManager : MonoBehaviour
{
    public List<InPlayCardController> souls = new List<InPlayCardController>();
    [SerializeField] private Transform underworldPosition;

    [HideInInspector] public PhotonView photonView;
    public static UnderworldManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        photonView = GetComponent<PhotonView>();
    }


    [PunRPC]
    public void AddToUnderworld(int PhotonId)
    {
        InPlayCardController card = PhotonView.Find(PhotonId).GetComponent<InPlayCardController>();
        souls.Add(card);

        card.transform.position = underworldPosition.position;

        card.transform.localScale = new Vector3(1.5f, 1.5f, 1f);


        if(card.cardData.script != "" && !card.cardData.keywords.Contains("Haunt"))
        {
            card.GetComponent<CardScript>().InPlayDeath();
        }
    }

    [PunRPC]
    public void ExorciseFromUnderworld(int PhotonId)
    {
        InPlayCardController card = PhotonView.Find(PhotonId).GetComponent<InPlayCardController>();

        if (souls.Contains(card))
        {
            souls.Remove(card);
        }

        if(card.cardData.script != "")
        {
            // card.GetComponent<CardScript>().OnExorcise();
            card.GetComponent<CardScript>().InPlayDeath();
        }

        // Probably delete card here
    }


}
