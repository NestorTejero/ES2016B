using System;
using UnityEngine;

public class Unit : MonoBehaviour, CanReceiveDamage, HUDSubject
{
    public float baseHealth;
    public Transform goal;

    private HealthComponent health;
    public float moveSpeed;
    public int purchaseCost;
    public int rewardCoins;
    public Weapon weapon;

    // Receive damage by weapon
    public void ReceiveDamage(float damage)
    {
        try
        {
            health.LoseHealth(damage);
            NotifyHUD();
            Debug.Log("UNIT " + name + " CURRENT_HEALTH: " + health.GetCurrentHealth());
        }
        catch (Exception)
        {
            NotifyHUD();
            GameController.instance.notifyDeath(this);
            Destroy(gameObject, 0.5f);
        }
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

    public void NotifyHUD()
    {
        var updateInfo = new HUDInfo
        {
            CurrentHealth = health.GetCurrentHealth(),
            TotalHealth = health.GetTotalHealth(),
            Damage = weapon.getCurrentDamage().ToString(),
            Range = weapon.getCurrentRange().ToString(),
            VisibleUpgradeButton = false
        };

        APIHUD.instance.notifyChange(this, updateInfo);
    }

    // Use this for initialization
    private void Start()
    {
        health = new HealthComponent(baseHealth);

        // Unit movement towards the goal
        var agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;

        weapon = gameObject.GetComponent<Weapon>();

        Debug.Log("UNIT CREATED");
    }

    // If enemy enters the range of attack
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Building>())
        {
            Debug.Log("Unit " + name + " Collision with Building");
            // Adds enemy to attack to the queue
            weapon.addTarget(col.gameObject.GetComponent<CanReceiveDamage>());
        }
    }

    // If enemy exits the range of attack
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<Building>())
            weapon.removeTarget(col.gameObject.GetComponent<CanReceiveDamage>());
    }

    public float GetDamage()
    {
        return weapon.getCurrentDamage();
    }
}