using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected int numCoins;

    public abstract void Play();

    public void addCoins(int coins)
    {
        numCoins += coins;
    }

    public void getMoney(Unit deadUnit)
    {
        Debug.Log("Oh! I've received " + deadUnit.rewardCoins + " coins! :D yay");
        addCoins(deadUnit.rewardCoins);
    }
}