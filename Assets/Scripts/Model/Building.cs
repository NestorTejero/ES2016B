using UnityEngine;

public class Building : MonoBehaviour, CanUpgrade, CanReceiveDamage
{
    public float baseHealth;
    private float currentHealth;
    private float currentLevel;
    public float repairCost;
    public float repairQuantity;
    private float totalHealth;
    public float upgradeCost;

    public float upgradeFactor;

    // Receive damage by weapon
    public void ReceiveDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Building's currentHealth: " + currentHealth);

        if (APIHUD.instance.getGameObjectSelected() == gameObject)
            APIHUD.instance.setHealth(currentHealth, totalHealth);

        if (currentHealth <= 0.0)
            GameController.instance.notifyDeath(this);
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
        totalHealth *= upgradeFactor;
        currentHealth *= upgradeFactor;
        currentLevel++;
        Debug.Log("BUILDING UPGRADED, now it has HP: " + totalHealth);
    }


    // Use this for initialization
    private void Start()
    {
        currentLevel = 0;
        currentHealth = baseHealth;
        totalHealth = baseHealth;
        Debug.Log("BUILDING CREATED with HP: " + baseHealth);
    }

    public bool IsRepairable(int numCoins)
    {
        return (repairCost <= numCoins) && (totalHealth > currentHealth);
    }

    // Repair the building
    public void Repair()
    {
        currentHealth += repairQuantity;
        if (currentHealth > totalHealth)
            currentHealth = totalHealth;
        Debug.Log("BUILDING REPAIRED, HP: " + currentHealth);
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }

    public float getTotalHealth()
    {
        return totalHealth;
    }
}