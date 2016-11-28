using UnityEngine;

public class Unit : MonoBehaviour, CanReceiveDamage
{
    public float baseHealth;
    private float currentHealth;
    // TODO This shouldn't be public
    public float damage;
    public Transform goal;
    public float moveSpeed;
    public int purchaseCost;
    public int rewardCoins;

    private float totalHealth;
    public Weapon weapon;

    // Receive damage by weapon
    public void ReceiveDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Unit " + name + " currentHealth: " + currentHealth);
        //Debug.Log("UNIT DAMAGED by HP: " + proj.getDamage());

        if (APIHUD.instance.getGameObjectSelected() == gameObject)
            APIHUD.instance.setHealth(currentHealth, totalHealth);

        if (currentHealth <= 0.0f)
        {
            GameController.instance.notifyDeath(this); // Tell controller I'm dead
            Destroy(gameObject, 0.5f);
        }
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }


    // Use this for initialization
    private void Start()
    {
        currentHealth = baseHealth;
        totalHealth = baseHealth;

        // Unit movement towards the goal
        var agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;

        weapon = gameObject.GetComponent<Weapon>();

        damage = weapon.baseDamage;

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

    public float getTotalHealth()
    {
        return totalHealth;
    }

    public float getCurrentHealth()
    {
        return currentHealth;
    }
}