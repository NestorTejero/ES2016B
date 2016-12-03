using UnityEngine;

public class Player : MonoBehaviour
{
    public int initialCoins;
    protected MoneyManager money;
    private ScoreManager score;

    private void Start()
    {
        money = new MoneyManager(initialCoins);
        APIHUD.instance.setMoney(money.GetNumCoins().ToString());
        score = new ScoreManager();
    }

    public void getMoney(Unit deadUnit)
    {
        Debug.Log("Oh! I've received " + deadUnit.rewardCoins + " coins! :D yay");
        money.AddDeadUnit(deadUnit);
        APIHUD.instance.setMoney(money.GetNumCoins().ToString());
        score.AddDeadUnit(deadUnit);
    }

    public void ChangeWave()
    {
        money.Add(100);
        APIHUD.instance.setMoney(money.GetNumCoins().ToString());
        score.Add(100);
    }
}