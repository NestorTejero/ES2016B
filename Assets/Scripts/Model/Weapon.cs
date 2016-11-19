using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour, CanUpgrade
{
    public float baseDamage;
    public float baseRange;
    public float baseCooldown;
	public float upgradeFactor;
    public AudioSource source_shoot, source_death;
    public AudioClip shootSound, deathSound1, deathSound2, deathSound3;

    private float currentDamage;
    private float currentRange;
    private float currentCooldown;
	private List<CanReceiveDamage> targets;
    

	// Use this for initialization
	void Start ()
	{
	    this.currentDamage = this.baseDamage;
	    this.currentRange = this.baseRange;
	    this.currentCooldown = this.baseCooldown;

		// Collider of the tower attached to this script
		this.gameObject.GetComponentInChildren<CapsuleCollider>().radius = this.currentRange;

		// List of targets assigned to the weapon
		this.targets = new List<CanReceiveDamage>();

		// Call Attack every 'cooldown' seconds
		InvokeRepeating("Attack", 0.0f, this.currentCooldown);

        // Set sounds
        //this.source_shoot = GetComponent<AudioSource>();

        Debug.Log ("WEAPON CREATED");
	}

	// Update is called once per frame
	void Update ()
	{

	}

	// Upgrade weapon features
	public void Upgrade ()
	{
		this.currentDamage *= upgradeFactor;
	}

	// Get weapon's current damage
    public float getCurrentDamage()
    {
        return this.currentDamage;
    }

	// Add target to list
	public void addTarget(CanReceiveDamage target){
		this.targets.Add (target);
		Debug.Log ("Targets to attack :" + targets.Count);
	}

	// Remove target from list
	public void removeTarget(CanReceiveDamage target){
		this.targets.Remove (target);
	}

	// Called to attack a target
	public void Attack()
	{
		// Checks if there is a target in the range
		if (this.targets.Count > 0) {

			// Get target to attack
			CanReceiveDamage target = targets [0];

			// Create the projectile that will go towards the target
			Projectile projectile = this.gameObject.AddComponent<Projectile>();
			projectile.create(1.0f, target, this.currentDamage);

			// Shoot the projectile
			bool dead = projectile.shoot ();
			if (dead) {
				this.targets.Remove (target);
                // Play death sound
                if (!this.source_death.isPlaying)
                    this.source_death.PlayOneShot(this.deathSound1);
            }

            // Play shoot sound
            if (!this.source_shoot.isPlaying)
            {
                this.source_shoot.PlayOneShot(this.shootSound);
            }
        }
	}
}

