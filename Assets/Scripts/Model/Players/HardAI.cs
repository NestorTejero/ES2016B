using UnityEngine;
using UnityEngine.VR;
using AssemblyCSharp;

public class HardAI : AI
{
	// Makes the movement according to this AI

	KnapsackProblem aiSolver = new KnapsackProblem();

	public override void Play(){
		InvokeRepeating("autoCoins", 0.0f, 1.0f);

		//create 200 units every wave
		UnitShop shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<UnitShop>();
		if (unitsWave < 200)
		{
			var availableUnits= shop.GetPurchasable(numCoins);

			/* AI methods */
			aiSolver.infoCalc (availableUnits, numCoins);
			var unitsToPurchase = aiSolver.Resolve ();

			/* We buy the units */
			for (int i = 0; i < unitsToPurchase.Count; i++) {
				var unit = unitsToPurchase[i];
				if (shop.IsPurchasable(unit, numCoins))
				{
					shop.Purchase(unit);
					Debug.Log("NEW UNIT " + unit.name + " PURCHASED.");
					unitsWave += 1;
					numCoins -= unit.purchaseCost;
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

	private void autoCoins(){
		//AI wins 20 coins every second
		numCoins += 20;
	}
}