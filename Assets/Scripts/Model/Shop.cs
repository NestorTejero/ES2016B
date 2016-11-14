using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Singleton Class that abstracts Shopping
 */

public class Shop : MonoBehaviour
{
    public GameObject building;

    public Transform[] respawns;
    public List<GameObject> units;

    //-------- UNIT PURCHASE --------//

    // Must be queried since it could change
    public List<Unit> getAvailableUnits()
    {
        var availableUnits = new List<Unit>();
        foreach (var o in units)
            availableUnits.Add(o.GetComponent<Unit>());
        return availableUnits;
    }

    public bool isUnitPurchasable(Unit unitToPurchase, int numCoins)
    {
        if (getAvailableUnits().Contains(unitToPurchase))
            return unitToPurchase.costCoins <= numCoins;
        return false;
    }

    public List<Unit> getPurchasableUnits(int numCoins)
    {
        return getAvailableUnits().Where(x => isUnitPurchasable(x, numCoins)).ToList();
    }

    public void purchaseUnit(Unit unitToPurchase)
    {
        // TODO do with tags
        var unitToPut = units.Where(x => x.GetComponent<Unit>().Equals(unitToPurchase)).ToList()[0];
        var spawnIndex = Random.Range(0, respawns.Length);
        Instantiate(unitToPut, respawns[spawnIndex].transform.position, respawns[spawnIndex].transform.rotation);
    }

    //-------- UPGRADE TOWER --------//

    // Must be queried since it could change
    public List<Tower> getAvailableTowers()
    {
        var towers = new List<Tower>();
        var objs = GameObject.FindGameObjectsWithTag("Tower");
        foreach (var o in objs)
            towers.Add(o.GetComponent<Tower>());
        return towers;
    }

    public bool isTowerUpgradeable(Tower tower, int numCoins)
    {
        if (getAvailableTowers().Contains(tower))
            return tower.upgradeCost <= numCoins;
        return false;
    }

    public List<Tower> getUpgradeableTowers(int numCoins)
    {
        return getAvailableTowers().Where(x => isTowerUpgradeable(x, numCoins)).ToList();
    }

    public void purchaseTowerUpgrade(Tower tower)
    {
        if (getAvailableTowers().Contains(tower))
            tower.Upgrade();
    }

    //-------- REPAIR BUILDING --------//

    public bool isBuildingRepairable(int numCoins)
    {
        var b = building.GetComponent<Building>();
        // Reparable if there's enough coins to repair and there's health missing
        return (b.repairCost <= numCoins) && (b.getMissingHealth() > 0);
    }

    public void purchaseBuildingRepair()
    {
        building.GetComponent<Building>().Repair();
    }
}