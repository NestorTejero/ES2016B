using AssemblyCSharp;
using UnityEngine;

public class HardAI : AI
{
    // Makes the movement according to this AI

    private readonly KnapsackProblem aiSolver = new KnapsackProblem();

    public override void Play()
    {
        //create 200 units every wave
        var shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<UnitShop>();
        if (unitsWave < 200)
        {
            var availableUnits = shop.GetPurchasable(money.GetNumCoins());

            /* AI methods */
            aiSolver.infoCalc(availableUnits, money.GetNumCoins());
            var unitsToPurchase = aiSolver.Resolve();

            /* We buy the units */
            for (var i = 0; i < unitsToPurchase.Count; i++)
            {
                var unit = unitsToPurchase[i];
                if (shop.IsPurchasable(unit, money.GetNumCoins()))
                {
                    shop.Purchase(unit);
                    Debug.Log("NEW UNIT " + unit.name + " PURCHASED.");
                    unitsWave += 1;
                    money.Spend(unit.purchaseCost);
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