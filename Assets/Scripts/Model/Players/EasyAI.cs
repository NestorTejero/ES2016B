using UnityEngine;

public class EasyAI : Player
{
    // Makes the movement according to this AI

    public override void Play()
    {
		InvokeRepeating ("autoCoins", 0.0f, 1.0f);
		var availableUnits = Shop.instance.getPurchasableUnits(numCoins);
        var unit = availableUnits[0];
		if (Shop.instance.isUnitPurchasable(unit, numCoins))
        {
            Shop.instance.purchaseUnit(unit);
            Debug.Log("new unit purchased");
        }
    }

	private void autoCoins(){
		//AI wins 20 coins every second
		numCoins += 20;
	}
		
}