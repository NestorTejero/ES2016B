using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	public int range;
	public int power;
	public int cooldown;

	//criticalMultiplier;
	//criticalChance;
	
	// Use this for initialization
	void Start ()
	{
		range = 3;
		power = 1;
		cooldown = 3;

		Debug.Log ("WEAPON");
	}

	// Update is called once per frame
	void Update ()
	{

	}
}
