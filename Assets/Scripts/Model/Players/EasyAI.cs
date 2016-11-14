using UnityEngine;
using UnityEngine.VR;

public class EasyAI : AI
{
    // Makes the movement according to this AI

	public override void Play(){
        InvokeRepeating("autoCoins", 0.0f, 1.0f);
        //create 20 units every wave
	    Shop shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<Shop>();
        if (unitsWave < 20)
        {
            var availableUnits = shop.getPurchasableUnits(numCoins);
            var unit = availableUnits[0];
            if (shop.isUnitPurchasable(unit, numCoins))
            {
                shop.purchaseUnit(unit);
                Debug.Log("new unit purchased");
                unitsWave += 1;
            }
        }
        else
        {
            if (!GameObject.FindObjectOfType(typeof(Unit)))
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