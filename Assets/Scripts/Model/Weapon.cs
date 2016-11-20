using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
    public float baseDamage;
    public float baseRange;
    public float baseCooldown;
    public float upgradeFactor;
    public AudioClip shootSound, deathSound1, deathSound2, deathSound3;
    public AudioSource source_shoot, source_death;
    public AudioClip[] death;
    public GameObject proj_obj; // Projectile prefab
    public GameObject proj_origin; // Projectile origin

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
        death = new AudioClip[]
        {
            (AudioClip)Resources.Load("Sound/Effects/Death 1"),
            (AudioClip)Resources.Load("Sound/Effects/Death 2"),
            (AudioClip)Resources.Load("Sound/Effects/Death 3")
        };

        Debug.Log ("WEAPON CREATED");
	}

    // Upgrade weapon features
    public void Upgrade()
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
		Debug.Log (this.gameObject.name + "-> Targets to attack :" + targets.Count);
	}

    // Remove target from list
    public void removeTarget(CanReceiveDamage target)
    {
        this.targets.Remove(target);
        // Play death sound
        if (!this.source_death.isPlaying)
        {
            //audio.PlayOneShot(list[number], 0.5f);
            source_death.PlayOneShot(death[Random.Range(0, death.Length)], 0.5f);
        }
    Debug.Log(this.gameObject.name + "-> Targets to attack :" + targets.Count);
    }

	// Get the available target to attack from the targets list
	public CanReceiveDamage getAvailableTarget(){

		// Checks if there is a target in the range
		while (this.targets.Count > 0) {

			// Get target to attack
			CanReceiveDamage target = this.targets [0];

			// Check if target is already dead
			if (target.Equals(null)) {
				Debug.Log (this.gameObject.name + ": TARGET ALREADY DEAD");
				this.removeTarget (target);
			} else {
				Debug.Log (this.gameObject.name + ": TARGET AVAILABLE TO SHOOT");
				return target;
			}
		}
		Debug.Log (this.gameObject.name + ": NO TARGETS ON QUEUE");
		return null;
	}

	// Called to attack a target
	public void Attack()
	{
		CanReceiveDamage target = getAvailableTarget();

		if (target != null)
		{

		    // Play shoot sound
		    if (!this.source_shoot.isPlaying)
		    {
			this.source_shoot.PlayOneShot(this.shootSound);
		    }
		    //Creates projectile with its properties and destroys it after 3 seconds
		    GameObject proj_clone = (GameObject) Instantiate (this.proj_obj, this.proj_origin.transform.position, this.proj_origin.transform.rotation);
		    proj_clone.GetComponent<Projectile> ().Properties (target, this.currentDamage);
		    Destroy (proj_clone, 3.0f);
		}
	}
}


