using UnityEngine;

public class EasyAI : Player
{
    // Makes the movement according to this AI

    public override void Play()
    {
		var availableUnits = Shop.instance.getPurchasableUnits((int)numCoins);
        var unit = availableUnits[0];
		if (Shop.instance.isUnitPurchasable(unit, (int)numCoins))
        {
            Shop.instance.purchaseUnit(unit);
            Debug.Log("new unit purchased");
        }
    }
	void Update()
	{
		//AI wins 20 coins x second
		numCoins += Time.deltaTime * 20;
	}
		
}