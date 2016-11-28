public class HealthComponent
{
    public float baseHealth;
    private float currentHealth;
    private float totalHealth;

    public HealthComponent(float baseHealth)
    {
        this.baseHealth = baseHealth;
        this.currentHealth = baseHealth;
        this.totalHealth = baseHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetTotalHealth()
    {
        return totalHealth;
    }

    public void LoseHealth(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0.0f)
        {
            throw new System.Exception();
        }
    }

    public void AddHealth(float healthQuantity)
    {
        currentHealth += healthQuantity;
        if (currentHealth > totalHealth)
        {
            currentHealth = totalHealth;
        }
    }

    public bool IsHealthFull()
    {
        return currentHealth == totalHealth;
    }

    public void Upgrade(float upgradeFactor)
    {
        totalHealth *= upgradeFactor;
        currentHealth *= upgradeFactor;
    }
}