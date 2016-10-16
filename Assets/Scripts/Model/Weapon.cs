using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour, CanUpgrade
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
