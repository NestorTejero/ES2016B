using UnityEngine;

public class EasyAI : AI
{
    // Makes the movement according to this AI

    public override void Play()
    {
        //create 20 units every wave
        var shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<UnitShop>();
        if (unitsWave < 20)
        {
            var availableUnits = shop.GetPurchasable(numCoins);
            var unit = availableUnits[0];
            if (shop.IsPurchasable(unit, numCoins))
            {
                shop.Purchase(unit);
                Debug.Log("NEW UNIT " + unit.name + " PURCHASED.");
                unitsWave += 1;
                numCoins -= unit.purchaseCost;
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