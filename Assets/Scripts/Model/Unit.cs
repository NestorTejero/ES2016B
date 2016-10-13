using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour, CanAttack
{
	public GameObject enemy;
	public int health;

	// Use this for initialization
	void Start ()
	{
		health = 10;
		enemy = this.gameObject;
		Debug.Log ("UNIT");
	}
	
	// Update is called once per frame
	void Update ()
	{
		// For testing
		enemy.transform.position += Vector3.forward * Time.deltaTime*2;	
	}

	// To attack wall when near enough
	public void Attack ()
	{

	}
}
