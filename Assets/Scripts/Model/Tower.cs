using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour, Upgradeable, CanAttack
{
	public GameObject tower;
	public GameObject enemy;
	public Weapon weapon;
	bool enemy_in = false;


	// Use this for initialization
	void Start ()
	{
		CapsuleCollider towerRange = tower.GetComponent<CapsuleCollider> ();
		weapon = new Weapon ();

		towerRange.radius = weapon.range;

		// invokes Attack every 3 seconds
		Invoke("Attack", weapon.cooldown);
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	// To upgrade when there are enough coins
	public void Upgrade ()
	{
		//radius up (more detection range)

		//weapon.range = weapon.range + 1
		//towerRange.radius = weapon.range;

	}

	// To attack enemies
	public void Attack ()
	{
		// cheks if there is an enemy in the range
		if(enemy_in){
			enemy.health = enemy.health - 1;
		}
	}

	public void OnTriggerEnter(Collider col){
		if (!enemy_in) {
			enemy_in = true;
			enemy = col.gameObject;
		}

		//enemy_in = false when enemy dies
	}
}
