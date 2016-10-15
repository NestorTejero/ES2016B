using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour, Upgradeable, CanAttack
{
	public GameObject tower;
	public CapsuleCollider towerRange;
	public Unit enemy;
	public Weapon weapon;
	public GameObject enemy_object;
	public bool enemy_in;
	public List<Unit> enemies;

	public float upgradeFactor;

	// Use this for initialization
	void Start ()
	{
		// gameObject attached to this script
		tower = this.gameObject;

		// Collider of the tower
		towerRange = tower.GetComponent<CapsuleCollider> ();

		// Assignes the radius of the collider as the weapon attack range
		towerRange.radius = weapon.range;

		// Fires every "cooldown of the weapon" seconds
		InvokeRepeating ("Attack", 0.0f, weapon.cooldown);

		// No enemy inside the range at the beginning
		enemy_in = false;

		// List of enemies when more than one enters the range
		enemies = new List<Unit> ();

		Debug.Log ("TOWER");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	// To upgrade when there are enough coins
	public void Upgrade ()
	{
		this.weapon.power *= this.upgradeFactor;
	}

	// To attack enemies
	public void Attack ()
	{
		// Checks if there is an enemy in the range
		if (enemy_in && enemy != null) {
			if (enemy.health > 1.0) {
				enemy.health = enemy.health - weapon.power;
				Debug.Log ("Enemy's health: " + enemy.health);
			} else {
				enemy_object = enemy.gameObject;
				Destroy (enemy_object);
				Debug.Log ("Enemy dead");

				if (enemies.Count > 0) {
					Debug.Log ("Another enemy");
					enemy = enemies [0];
					enemies.Remove (enemy);
				} else {
					enemy_in = false;
				}
			}
		}
	}

	void OnTriggerEnter (Collider col)
	{
		Debug.Log ("Collision");
		if (!enemy_in) {
			enemy_in = true;
			Debug.Log ("Enemy In");

			// Gets the script of the Collider that entered the radius of the tower
			enemy = col.gameObject.GetComponent<Unit> ();

		} else {
			// Adds enemy to attack to the queue
			enemies.Add (col.gameObject.GetComponent<Unit> ());
			Debug.Log ("Enemy in queue");
		}
			
	}
}
