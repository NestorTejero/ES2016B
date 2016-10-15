using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour, CanAttack, Attackable
{
	public GameObject enemy;
	public float health;

	// Use this for initialization
	void Start ()
	{
		health = 10.0f;
		enemy = this.gameObject;
		Debug.Log ("UNIT");
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
	}
}
