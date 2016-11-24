using UnityEngine;
using UnityEngine.VR;

public class MediumAI : AI
{
    // Makes the movement according to this AI

    public override void Play()
    {
        //create 70 units every wave
        UnitShop shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<UnitShop>();
        if (unitsWave < 70)
        {
            /* we buy the unit with less damage */
            var availableUnits = shop.GetPurchasable(numCoins);
            var unit = availableUnits[0];
            if (shop.IsPurchasable(unit, numCoins))
            {
                shop.Purchase(unit);
                Debug.Log("NEW UNIT " + unit.name + " PURCHASED.");
                unitsWave += 1;
                numCoins -= unit.purchaseCost;
            }

            /* we look for buy a second unit, this time is random */
            availableUnits = shop.GetPurchasable(numCoins);
            unit = availableUnits[Random.Range(0, availableUnits.Count)];
            if (shop.IsPurchasable(unit, numCoins))
            {
                shop.Purchase(unit);
                Debug.Log("NEW UNIT " + unit.name + " PURCHASED.");
                unitsWave += 1;
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