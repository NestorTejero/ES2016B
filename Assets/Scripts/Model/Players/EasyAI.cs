using UnityEngine;

public class EasyAI : Player
{
    // Makes the movement according to this AI

    public override void Play()
    {
        var availableUnits = Shop.instance.getPurchasableUnits(numCoins);
        var unit = availableUnits[0];
        if (Shop.instance.isUnitPurchasable(unit, numCoins))
        {
            Shop.instance.purchaseUnit(unit);
            Debug.Log("new unit purchased");
        }
    }
}