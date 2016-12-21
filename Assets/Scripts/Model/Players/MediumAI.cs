using UnityEngine;

public class MediumAI : AI
{
    // Makes the movement according to this AI

    public override void Play()
    {
        //create 70 units every wave
        var shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<UnitShop>();
        if (unitsWave < 70)
        {
            /* we buy the unit with less damage */
            var availableUnits = shop.GetPurchasable(numCoins);
			if (availableUnits.Count > 0) {
				var unit = availableUnits [0];
				if (shop.IsPurchasable (unit, numCoins)) {
					shop.Purchase (unit);
					unitsWave += 1;
					numCoins -= unit.purchaseCost;
				}
			}
				if (availableUnits.Count > 0)
				{
	            /* we look for buy a second unit, this time is random */
	            availableUnits = shop.GetPurchasable(numCoins);
	            var unit = availableUnits[Random.Range(0, availableUnits.Count)];
	            if (shop.IsPurchasable(unit, numCoins))
	            {
	                shop.Purchase(unit);
	                unitsWave += 1;
	            }
			}
        }
        else
        {
            if (GameObject.FindGameObjectsWithTag("Unit").Length == 0)
            {
                GameController.instance.notifyWaveClear(this);
                unitsWave = 0;
            }
        }
    }
}