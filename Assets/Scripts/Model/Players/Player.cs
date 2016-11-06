using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    protected List<Unit> availableUnits;
    public int initialCoins;
    protected int numCoins;

    public abstract void Play();

    private void Start()
    {
        numCoins = initialCoins;
        InvokeRepeating("Play", 3.0f, 2.0f);
    }

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