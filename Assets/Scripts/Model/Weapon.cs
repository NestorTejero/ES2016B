using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour, CanUpgrade
{
    public float baseDamage;
    public float baseRange;
    public float baseCooldown;
	public float upgradeFactor;
	public AudioClip shootSound;
	public GameObject proj_obj; // Projectile prefab
	public GameObject proj_origin; // Projectile origin 

    private float currentDamage;
    private float currentRange;
    private float currentCooldown;
	private List<CanReceiveDamage> targets;
    private AudioSource source;

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
        this.source = GetComponent<AudioSource>();

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
		Debug.Log (this.gameObject.name + "-> Targets to attack :" + targets.Count);
	}

	// Remove target from list
	public void removeTarget(CanReceiveDamage target){
		this.targets.Remove (target);
		Debug.Log (this.gameObject.name + "-> Targets to attack :" + targets.Count);
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

		if (target != null) {
			// Play sound
			this.source.PlayOneShot (this.shootSound);

			//Creates projectile with its properties and destroys it after 3 seconds
			GameObject proj_clone = (GameObject) Instantiate (this.proj_obj, this.proj_origin.transform.position, this.proj_origin.transform.rotation);
			proj_clone.GetComponent<Projectile> ().Properties (target, this.currentDamage);
			Destroy (proj_clone, 3.0f);
		}
	}
}

