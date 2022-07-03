using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameActionList : MonoBehaviour
{
    public List<SequenceSystem.GameAction> actions = new List<SequenceSystem.GameAction>();

    [PunRPC]
    public void AddNetworkedActionToSequence(int indexToRun)
    {
        actions[indexToRun].sendAction = false;
        MainSequenceManager.Instance.Add(actions[indexToRun]);
    }
}
