using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour, CanUpgrade
{
	public Weapon weapon;
	private int currentLevel;
	private List<CanReceiveDamage> targets;
    
    // Use this for initialization
    void Start ()
    {
        this.currentLevel = 0;
		this.targets = new List<CanReceiveDamage>();
		this.weapon.setTarget (targets);
		Debug.Log ("TOWER CREATED");
	}
		
    // Update is called once per frame
    void Update ()
	{
		
	}

	// To upgrade when there are enough coins
	public void Upgrade ()
	{
		this.weapon.Upgrade();
	    this.currentLevel++;
		Debug.Log ("TOWER UPGRADED, Power: " + this.weapon.getCurrentDamage());
	}
}
