using UnityEngine;

/**
 * Class that represents a Player, let it be on enemy or friendly side
 */

public class Player
{
    private readonly PlayerLogic logic;

    public Player(PlayerLogic logic)
    {
        this.logic = logic;
    }

    public void getMoney(Unit deadUnit)
    {
        Debug.Log("Oh! I've received " + deadUnit.rewardCoins + " coins! :D yay");
        logic.addCoins(deadUnit.rewardCoins);
    }
}