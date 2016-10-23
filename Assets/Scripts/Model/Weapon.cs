using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour, CanUpgrade
{
    public float baseDamage;
    public float baseRadius;
    public float baseRange;
    public float baseCooldown;

    // TODO change all these to private
    public float currentDamage; 
    public float currentRadius;
    public float currentRange;
    public float currentCooldown;
    //

    public float upgradeFactor;

	// Use this for initialization
	void Start ()
	{
	    this.currentDamage = this.baseDamage;
	    this.currentRadius = this.baseRadius;
	    this.currentRange = this.baseRange;
	    this.currentCooldown = this.baseCooldown;
	}

	// Update is called once per frame
	void Update ()
	{

	}

	public void Upgrade ()
	{
		this.currentDamage *= upgradeFactor;
	}

    /*public float getDamage()
    {
        return this.currentDamage;
    }*/
}
