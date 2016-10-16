using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour, CanAttack, CanReceiveDamage
{

	public float maxHealth;
	public float health; // TODO change to private

	// Use this for initialization
	void Start ()
	{
		health = maxHealth;
		Debug.Log ("UNIT CREATED");
	}
	
	// Update is called once per frame
	void Update ()
	{
		// For testing
		this.gameObject.transform.position += Vector3.forward * Time.deltaTime * 2;	

	}

	// To attack wall when near enough
	public void Attack ()
	{

	}

	// To upgrade when there are enough coins
	public void ReceiveDamage (Weapon wep)
	{
		this.health -= wep.power;
		if (this.health <= 0.0f) {
			// TODO implement death and stuff here
				// The logic that destroys the enemy shouldn't go inside Tower	
		}
		Debug.Log ("UNIT DAMAGED by HP: " + wep.power);
	}
}
