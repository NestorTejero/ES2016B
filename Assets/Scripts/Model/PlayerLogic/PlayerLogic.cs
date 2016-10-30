using UnityEngine;

public abstract class PlayerLogic : MonoBehaviour
{
    protected int numCoins;

    public abstract void Play();

    public void addCoins(int coins)
    {
        numCoins += coins;
    }
}