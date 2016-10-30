using UnityEngine;

/**
 * Class that represents a Player, let it be on enemy or friendly side
 */

public class Player : MonoBehaviour
{
    private PlayerLogic logic;
    private int numCoins;
    // Use this for initialization
    void Start()
    {
        numCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void setLogic(PlayerLogic logic)
    {
        this.logic = logic;
    }

    public void notifyDeath(Unit deadUnit)
    {
        Debug.Log("Oh! I've received " + deadUnit.rewardCoins + " money! :D yay");
        numCoins += deadUnit.rewardCoins;
    }
}