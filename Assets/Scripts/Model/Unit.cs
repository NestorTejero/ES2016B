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
    private List<CanReceiveDamage> targets;

	// Use this for initialization
	void Start ()
	{
		this.currentHealth = this.baseHealth;
        this.totalHealth = this.baseHealth;

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = this.goal.position;

		// List of targets assigned to the unit weapon
		this.targets = new List<CanReceiveDamage>();
		this.weapon.setTarget (targets);

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
		Debug.Log("UNIT DAMAGED by HP: " + wep.getCurrentDamage());

		if (this.currentHealth <= 0.0f) {
			Destroy (this.gameObject);
			Debug.Log ("Unit " + this.name + " is dead");
			return true;
		}
		return false;
	}
}
