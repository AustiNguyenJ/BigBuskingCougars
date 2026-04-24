using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int money;

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public bool SpendMoney(int amount)
    {
        if (money < amount) return false;
        money -= amount;
        return true;
    }

    public int CheckMoney()
    {
        return money;
    }
}