using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Class that makes Unit creation a service
 */

public class UnitShop
{
    private readonly List<Unit> availableUnits;
    // TODO list of positions to spawn Units

    public UnitShop()
    {
        availableUnits = new List<Unit>();
        var objs = GameObject.FindGameObjectsWithTag("Unit");
        foreach (var o in objs)
            availableUnits.Add(o.GetComponent<Unit>());
    }

    public List<Unit> getAvailableUnits()
    {
        return availableUnits;
    }


    public bool isUnitPurchasable(Unit unitToPurchase, int numCoins)
    {
        if (availableUnits.Contains(unitToPurchase))
            return unitToPurchase.costCoins <= numCoins;
        return false;
    }

    public List<Unit> getPurchasableUnits(int numCoins)
    {
        return availableUnits.Where(x => isUnitPurchasable(x, numCoins)).ToList();
    }

    public void purchaseUnit(Unit unitToPurchase)
    {
        if (availableUnits.Contains(unitToPurchase))
        {
            // TODO Missing how to spawn it somewhere
        }
    }
}