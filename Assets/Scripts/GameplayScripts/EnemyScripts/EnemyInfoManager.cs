using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

// Pretend this says InfoManager
public class EnemyInfoManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemyName;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI blueEnergy;
    [SerializeField] private TextMeshProUGUI redEnergy;
    [SerializeField] private TextMeshProUGUI greenEnergy;
    [SerializeField] private TextMeshProUGUI blackEnergy;

    public static EnemyInfoManager Instance { get; private set; }

    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
        }

        if (PhotonNetwork.IsConnected)
        {
            foreach(Photon.Realtime.Player player in PhotonNetwork.PlayerList)
            {
                if(player != PhotonNetwork.LocalPlayer)
                {
                    enemyName.text = player.NickName;
                }
                else
                {
                    playerName.text = player.NickName;
                }
            }

            blueEnergy.text = "0/0";
            redEnergy.text = "0/0";
            greenEnergy.text = "0/0";
            blackEnergy.text = "0/0";
        }
    }

    public void SetTotalEnergy(string color, int total, int current)
    {
        string energyString = current.ToString() + "/" + total.ToString();

        if (color == "Blue")
        {
            blueEnergy.text = energyString;
        }
        else if(color == "Red")
        {
            redEnergy.text = energyString;
        }
        else if(color == "Green")
        {
            greenEnergy.text = energyString;
        }
        else if(color == "Black")
        {
            blackEnergy.text = energyString;
        }
    }
}
