using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour, Upgradeable
{
	public float range;
	public float power;
	public float cooldown;
	public float upgradeFactor;

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

	public void Upgrade ()
	{
		this.power *= upgradeFactor;
	}
}
