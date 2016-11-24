using UnityEngine;

public class Tower : MonoBehaviour, CanUpgrade
{
    private int currentLevel;
    public int upgradeCost;
    private Weapon weapon;

    public bool IsUpgradeable(int numCoins)
    {
        return upgradeCost <= numCoins;
    }

    // To upgrade when there are enough coins
    public void Upgrade()
    {
        weapon.Upgrade();
        currentLevel++;
        Debug.Log("TOWER UPGRADED, Power: " + weapon.getCurrentDamage());
    }

    // Use this for initialization
    private void Start()
    {
        weapon = gameObject.GetComponent<Weapon>();
        currentLevel = 0;
        Debug.Log("TOWER CREATED");
    }

    // If enemy enters the range of attack
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("Tower " + name + " Collision with Unit");

            // Adds enemy to attack to the queue
            weapon.addTarget(col.gameObject.GetComponentInParent<CanReceiveDamage>());
        }
    }

    // If enemy exits the range of attack
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
            weapon.removeTarget(col.gameObject.GetComponentInParent<CanReceiveDamage>());
    }
}