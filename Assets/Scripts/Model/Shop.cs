using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
 * Singleton Class that abstracts Shopping
 */

public class Shop : MonoBehaviour
{
    public static Shop instance;
    private Building building;
    // TODO list of positions to spawn Units
	public GameObject[] respawns;


    private void Start()
    {
        building = GameObject.FindGameObjectWithTag("Building").GetComponent<Building>();
		respawns = GameObject.FindGameObjectsWithTag ("SpawnP");
    }

    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    //-------- UNIT PURCHASE --------//

    // Must be queried since it could change
    public List<Unit> getAvailableUnits()
    {
        var availableUnits = new List<Unit>();
        var objs = GameObject.FindGameObjectsWithTag("PurchasableUnit");
        foreach (var o in objs)
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
        // TODO Missing how to spawn it somewhere
		foreach (GameObject respawn in respawns) {
			Instantiate (unitToPurchase, respawn.transform.position, respawn.transform.rotation);
			Debug.Log("new unit purchasedddddddddddddddddddddddddd");
		}

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
        {
            tower.Upgrade();
        }
    }

    //-------- REPAIR BUILDING --------//

    public bool isBuildingRepairable(int numCoins)
    {
        // Reparable if there's enough coins to repair and there's health missing
        return (building.repairCost <= numCoins) && (building.getMissingHealth() > 0);
    }

    public void purchaseBuildingRepair()
    {
        building.Repair();
    }
}