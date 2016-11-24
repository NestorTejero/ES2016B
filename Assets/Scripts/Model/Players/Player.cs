using UnityEngine;

public class Player : MonoBehaviour
{
    public int initialCoins;
    protected int numCoins, unitsWave;

    public void addCoins(int coins)
    {
        numCoins += coins;
        APIHUD.instance.setMoney(numCoins.ToString());
    }

    public void getMoney(Unit deadUnit)
    {
        Debug.Log("Oh! I've received " + deadUnit.rewardCoins + " coins! :D yay");
        addCoins(deadUnit.rewardCoins);
    }
}