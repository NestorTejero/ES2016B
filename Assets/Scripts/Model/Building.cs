using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour, CanUpgrade, CanRepair, CanReceiveDamage
{
	public float maxHealth;
	private float health;
	public float upgradeHealthFactor;
	public float repairHealthQuantity;

	// Use this for initialization
	void Start ()
	{
		this.health = this.maxHealth;
		Debug.Log ("BUILDING CREATED with HP: " + this.maxHealth);
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
		Debug.Log ("BUILDING UPGRADED, now it has HP: " + this.maxHealth);
	}

	public void Repair ()
	{
		this.health += this.repairHealthQuantity;
		if (this.health > this.maxHealth) {
			this.health = this.maxHealth;
		}
		Debug.Log ("BUILDING REPAIRED, HP: " + this.health);
	}

	public void ReceiveDamage (Weapon wep)
	{
		this.health -= wep.power;
		if (this.health <= 0.0) {
			// TODO put winning / losing condition logic here
		}
		Debug.Log ("BUILDING DAMAGED by HP: " + wep.power);
	}
}
