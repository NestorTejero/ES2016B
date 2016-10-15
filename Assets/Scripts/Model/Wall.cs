using UnityEngine;
using System.Collections;
using System;

public class Wall : MonoBehaviour, Upgradeable, Repairable, Attackable
{
	public float maxHealth;
	public float health;
	public float upgradeHealthFactor;
	public float repairHealthQuantity;

	// Use this for initialization
	void Start ()
	{
        
	}

	// Update is called once per frame
	void Update ()
	{

	}

	// To upgrade when there are enough coins
	public void Upgrade ()
	{
		this.maxHealth *= this.upgradeHealthFactor;
		this.health *= this.upgradeHealthFactor;
		Debug.Log ("WALL UPGRADED");
	}

	public void Repair ()
	{
		this.health += this.repairHealthQuantity;
		if (this.health > this.maxHealth) {
			this.health = this.maxHealth;
		}
		Debug.Log ("WALL REPAIRED");
	}

	public void ReceiveDamage (Weapon wep)
	{
		this.health -= wep.power;
		if (this.health <= 0.0) {
			// TODO put winning / losing condition logic here
		}
		Debug.Log ("WALL DAMAGED");
	}
}
