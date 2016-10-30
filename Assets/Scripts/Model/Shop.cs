using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Class that makes Unit creation a service
 */

public class Shop
{
    private readonly List<Unit> availableUnits;
    private readonly Building building;
    // TODO list of positions to spawn Units

    public Shop()
    {
        availableUnits = new List<Unit>();
        var objs = GameObject.FindGameObjectsWithTag("PurchasableUnit");
        foreach (var o in objs)
            availableUnits.Add(o.GetComponent<Unit>());

        building = GameObject.FindGameObjectWithTag("Building").GetComponent<Building>();
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
        // TODO Missing how to spawn it somewhere
    }

    public bool isBuildingRepairable(int numCoins)
    {
        // Reparable if there's enough coins to repair and there's health missing
        return (building.repairCost <= numCoins) && (building.getMissingHealth() > 0);
    }

    public void repairBuilding()
    {
        building.Repair();
    }
}