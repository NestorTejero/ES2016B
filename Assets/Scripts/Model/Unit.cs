using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour, CanAttack, Attackable
{
	public GameObject enemy;
	public float maxHealth;
	public float health;

	// Use this for initialization
	void Start ()
	{
		maxHealth = 10.0f;
		health = maxHealth;
		enemy = this.gameObject;
		Debug.Log ("UNIT CREATED");
	}
	
	// Update is called once per frame
	void Update ()
	{
		// For testing
		enemy.transform.position += Vector3.forward * Time.deltaTime * 2;	
	}

	// To attack wall when near enough
	public void Attack ()
	{

	}

	// To upgrade when there are enough coins
	public void ReceiveDamage (Weapon wep)
	{
		// TODO implement death and stuff here
		// Issue WeaponLogic to be fixed
		// The logic that destroys the enemy shouldn't go inside tower
		this.health -= wep.power;
		if (this.health <= 0.0f) {
			// TODO unit destruction logic (should get it out of Tower)
		}
		Debug.Log ("UNIT DAMAGED");
	}
}
