using UnityEngine;

public class EasyAI : Player
{

    // Makes the movement according to this AI

	public override void Play(){
		//Empty method
	}

    public override void PlayAI()
    {
		InvokeRepeating ("autoCoins", 0.0f, 1.0f);
		//create 20 units every wave
		if (unitsWave < 20) {
			var availableUnits = Shop.instance.getPurchasableUnits (numCoins);
			var unit = availableUnits [0];
			if (Shop.instance.isUnitPurchasable (unit, numCoins)) {
				Shop.instance.purchaseUnit (unit);
				Debug.Log ("new unit purchased");
				unitsWave += 1;
			}
		}else{
			if(!GameObject.FindObjectOfType(typeof(Unit))){
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