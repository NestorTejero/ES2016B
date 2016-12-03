using System;
using UnityEngine;

public class Building : MonoBehaviour, CanUpgrade, CanReceiveDamage, HUDSubject
{
    public float baseHealth;
    private float currentLevel;

    public HealthComponent health;
    public float repairCost;
    public float repairQuantity;
    public float upgradeCost = 1100;
    public float upgradeFactor;

    // Receive damage by weapon
    public void ReceiveDamage(float damage)
    {
        try
        {
            health.LoseHealth(damage);
            NotifyHUD();
            Debug.Log("BUILDING RECEIVED DAMAGE: " + damage + " - CURRENT_HEALTH: " + health.GetCurrentHealth());
        }
        catch (Exception)
        {
            NotifyHUD();
            GameController.instance.notifyDeath(this);
        }
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

    public bool IsUpgradeable(int numCoins)
    {
        return upgradeCost <= numCoins;
    }

    // To upgrade when there are enough coins
    public void Upgrade()
    {
        health.Upgrade(upgradeFactor);
        currentLevel++;
        NotifyHUD();
        Debug.Log("BUILDING UPGRADED, TOTAL HP: " + health.GetTotalHealth());
    }

    public void NotifyHUD()
    {
        var updateInfo = new HUDInfo
        {
            CurrentHealth = health.GetCurrentHealth(),
            TotalHealth = health.GetTotalHealth(),
			VisibleUpgradeButton = IsUpgradeable(GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().getMoney()),
			VisibleRepairButton = IsRepairable(GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().getMoney())
        };

        APIHUD.instance.notifyChange(this, updateInfo);
    }

    // Use this for initialization
    private void Start()
    {
        currentLevel = 0;
        health = new HealthComponent(baseHealth);
        Debug.Log("BUILDING CREATED with HP: " + baseHealth);
    }

    public bool IsRepairable(int numCoins)
    {
        return (repairCost <= numCoins) && health.IsHealthFull() == false;
    }

    // Repair the building
    public void Repair()
    {
        health.AddHealth(repairQuantity);
        NotifyHUD();
        Debug.Log("BUILDING REPAIRED, CURRENT HP: " + health.GetCurrentHealth());
    }
}