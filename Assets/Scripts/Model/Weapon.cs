using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon : MonoBehaviour, CanUpgrade
{
    public float baseDamage;
    public float baseRange;
    public float baseCooldown;
	public float upgradeFactor;

    private float currentDamage;
    private float currentRange;
    private float currentCooldown;

	private CanReceiveDamage target;
	private Projectile projectile;


	//////////////////////////////////////////
	private CapsuleCollider attackZone;
	public AudioClip shootSound;
	private GameObject target_object;
	private bool target_in;
	private List<CanReceiveDamage> targets;
	private AudioSource source;
	//////////////////////////////////////////



	// Use this for initialization
	void Start ()
	{
	    this.currentDamage = this.baseDamage;
	    this.currentRange = this.baseRange;
	    this.currentCooldown = this.baseCooldown;

		//////////////////////////////////////////////////////////
		// No target inside the range at the beginning
		target_in = false;

		// List of targets when more than one enters the range
		targets = new List<CanReceiveDamage> ();

		// Collider of the tower attached to this script set to the weapon attack zone
		attackZone = this.gameObject.GetComponent<CapsuleCollider> ();
		attackZone.radius = this.currentRange;
		//////////////////////////////////////////////////////////

		InvokeRepeating("Attack", 0.0f, this.currentCooldown);
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
		source = GetComponent<AudioSource>();
	}

	// Getters
    public float getCurrentDamage()
    {
        return this.currentDamage;
    }

	public float getCurrentCooldown()
	{
		return this.currentCooldown;
	}

	// Setters
	public void setTarget(CanReceiveDamage tar){
		this.target = tar;
	}

	public void setAttackZone(CapsuleCollider coll){
		this.attackZone = coll;
	}
		
	// Called to attack a target
	public void Attack()
	{
		// Checks if there is a target in the range
		if (target_in && target != null) {
			Awake ();
			//projectile = new Projectile (1.0f, target);
			projectile = this.gameObject.AddComponent<Projectile>();
			projectile.initialize (1.0f, target);
			target.ReceiveDamage (this);

			float vol = Random.Range (.5f, 1.0f);
			source.PlayOneShot (shootSound, vol);

			if (targets.Count > 0) {
				Debug.Log ("Another enemy");
				target = targets [0];
				targets.Remove (target);
			} else {
				target_in = false;
			}
		}
	}
		
		
	void OnTriggerEnter (Collider col)
	{
		Debug.Log ("Collision");
		if (!target_in) {
			target_in = true;
			Debug.Log ("Target In");

			// Gets the script of the Collider that entered the attack zone of the weapon
			target = col.gameObject.GetComponent<CanReceiveDamage> ();

		} else {
			// Adds enemy to attack to the queue
			targets.Add (col.gameObject.GetComponent<CanReceiveDamage> ());
			Debug.Log ("Target in queue");
		}

	}
}

