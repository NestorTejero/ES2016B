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
		this.gameObject.GetComponent<CapsuleCollider> ().radius = this.currentRange;

		// List of targets assigned to the weapon
		this.targets = new List<CanReceiveDamage>();

		InvokeRepeating("Attack", 0.0f, this.currentCooldown);

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
		
	//USeed for sounds
	void Awake()
	{
		this.source = GetComponent<AudioSource>();
	}

	// Getters
    public float getCurrentDamage()
    {
        return this.currentDamage;
    }

	public void addTarget(CanReceiveDamage target){
		this.targets.Add (target);
		Debug.Log ("Target in queue");
	}

	// Called to attack a target
	public void Attack()
	{
		// Checks if there is a target in the range
		if (this.targets.Count > 0) {
			Awake ();
			//Debug.Log ("targets num :" + targets.Count);
			CanReceiveDamage target = targets [0];
			Projectile projectile = this.gameObject.AddComponent<Projectile>();
			projectile.initialize (1.0f, target);
			bool dead = target.ReceiveDamage (this);
			if (dead) {
				this.targets.Remove (target);
			}
			float vol = Random.Range (.5f, 1.0f);
			this.source.PlayOneShot (this.shootSound, vol);
		}
	}
}

