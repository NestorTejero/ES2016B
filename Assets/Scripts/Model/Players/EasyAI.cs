using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EasyAI : Player
{
    // Makes the movement according to this AI
	//public Unit unitToPurchase;
	private List<Unit> availableUnits;

    private void Start()
    {
        numCoins = 100;
		//unitToPurchase = new Unit ();
		InvokeRepeating ("Play", 3.0f, 2.0f);
    }

    private void Update()
    {
		numCoins += (int)Time.deltaTime * 100;
		availableUnits = Shop.instance.getPurchasableUnits (numCoins);

	
    }

    public override void Play()
    {
		//var availableUnits = new List<Unit>();
		//var objs = GameObject.FindGameObjectsWithTag("PurchasableUnit");
		//foreach (var o in objs)
		//	availableUnits.Add(o.GetComponent<Unit>());

		//if (availableUnits.Contains(unitToPurchase)){
		//	Shop.instance.purchaseUnit (unitToPurchase);
		//	Debug.Log("new unit purchased");

		availableUnits = Shop.instance.getPurchasableUnits (numCoins);
		Unit unit = availableUnits[Random.Range(0,availableUnits.Count)];
		if(Shop.instance.isUnitPurchasable(unit, numCoins)){
			Shop.instance.purchaseUnit(unit);
			Debug.Log("new unit purchased");

		}
    }
}