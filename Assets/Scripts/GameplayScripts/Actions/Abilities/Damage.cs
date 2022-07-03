using System.Collections;

public class Damage : TargetedAbility
{

    public int amount = 0;

    public delegate int GetAmount();
    GetAmount getAmount;

    public void Initialize(int damage)
    {
        amount = damage;
        getAmount = DefaultGetAmount;
    }

    public void Initialize(GetAmount newGetAmount)
    {
        getAmount = newGetAmount;
    }

    public int DefaultGetAmount()
    {
        return amount;
    }

    public override void SetUpAbility(string[] str_args) 
    {
        amount = str_args[0].ConvertToInt();
    }

    public override IEnumerator ActionCoroutine()
    {
        // Deal damage to target
        object[] rpcData = { getAmount() };
        target.GetComponent<InPlayCardController>().photonView.RPC("RPCTakeDamage", Photon.Pun.RpcTarget.All, rpcData);
        OnEnd();
        return null;
    }


}
