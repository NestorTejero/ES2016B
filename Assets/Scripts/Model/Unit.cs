using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour, CanReceiveDamage
{
	public float baseHealth;
    private float totalHealth;
    public float currentHealth; // TODO change to private
    public float moveSpeed;
    public int costCoins;
    public int rewardCoins;

    //TODO REFACTOOOOR PLZ
    public Transform goal;
    public Weapon weapon;
    public GameObject objectTarget;
    private Building target;
	// Use this for initialization
	void Start ()
	{
		this.currentHealth = this.baseHealth;
        this.totalHealth = this.baseHealth;

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.position;
        target = objectTarget.gameObject.GetComponent<Building>();

        InvokeRepeating("Attack", 0.0f, 1.0f);
        Debug.Log ("UNIT CREATED");
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	// To attack wall when near enough
    // TODO Move Attack inside Weapon
	public void Attack ()
	{
        target.currentHealth -= 1.0f;
        Debug.Log("Unit: " + this.name + "attaks" + target.name);
        Debug.Log("Target Health" + target.currentHealth);
    }

	public void ReceiveDamage (Weapon wep)
	{
		this.currentHealth -= wep.currentDamage;
		if (this.currentHealth <= 0.0f) {
			// TODO implement death and stuff here
				// The logic that destroys the Unit object shouldn't go inside Tower	
                // The enemy should destroy itself
		}
		Debug.Log ("UNIT DAMAGED by HP: " + wep.currentDamage);
	}
}
