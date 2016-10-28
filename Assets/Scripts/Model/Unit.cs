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
	public Weapon weapon;

	private float totalHealth;
	private float currentHealth;

	// Use this for initialization
	void Start ()
	{
		this.currentHealth = this.baseHealth;
        this.totalHealth = this.baseHealth;

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = this.goal.position;

        Debug.Log ("UNIT CREATED");
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public bool ReceiveDamage (Weapon wep)
	{
		this.currentHealth -= wep.getCurrentDamage();
		Debug.Log ("Unit " + this.name +" currentHealth: " + this.currentHealth);
		//Debug.Log("UNIT DAMAGED by HP: " + wep.getCurrentDamage());

		if (this.currentHealth <= 0.0f) {
			Destroy (this.gameObject);
			Debug.Log ("Unit " + this.name + " is dead");
			return true;
		} else {
			return false;
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.GetComponent<Building> ()) {
			Debug.Log ("Unit Collision with Building");

			// Adds enemy to attack to the queue
			this.weapon.addTarget (col.gameObject.GetComponent<CanReceiveDamage> ());
		}
	}
}
