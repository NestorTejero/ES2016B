using UnityEngine;

public abstract class PlayerLogic : MonoBehaviour
{
    private int numCoins;

    public PlayerLogic()
    {
        numCoins = 0;
    }

    public abstract void Play();

    public void addCoins(int coins)
    {
        numCoins += coins;
    }
}