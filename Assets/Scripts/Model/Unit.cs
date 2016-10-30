using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour, CanReceiveDamage
{
    public float baseHealth;
    public float moveSpeed;
    public int costCoins;
    public int rewardCoins;
    public Transform goal;
    private Weapon weapon;

    private float totalHealth;
	private float currentHealth;

	// Use this for initialization
	void Start ()
	{
		this.currentHealth = this.baseHealth;
        this.totalHealth = this.baseHealth;

		// Unit movement towards the goal
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = this.goal.position;

	    this.weapon = this.gameObject.GetComponent<Weapon>();

        Debug.Log ("UNIT CREATED");
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	// Receive damage from a projectile (shot by weapon)
	public bool ReceiveDamage (Projectile proj)
	{
		this.currentHealth -= proj.getDamage();
		Debug.Log ("Unit " + this.name +" currentHealth: " + this.currentHealth);
		//Debug.Log("UNIT DAMAGED by HP: " + proj.getDamage());

		if (this.currentHealth <= 0.0f) {
			Destroy (this.gameObject);
			Debug.Log ("Unit " + this.name + " is dead");
			return true;
		} else {
			return false;
		}
	}

	// If enemy enters the range of attack
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.GetComponent<Building> ()) {
			Debug.Log ("Unit " + this.name + " Collision with Building");
			// Adds enemy to attack to the queue
			this.weapon.addTarget (col.gameObject.GetComponent<CanReceiveDamage> ());
		}
	}

	// If enemy exits the range of attack
	void OnTriggerExit (Collider col)
	{
		if (col.gameObject.GetComponent<Building> ()) {
			// Removes enemy to attack from the queue
			this.weapon.removeTarget (col.gameObject.GetComponent<CanReceiveDamage> ());
		}
	}
}
