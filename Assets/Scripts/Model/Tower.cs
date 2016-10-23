using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour, CanUpgrade
{
    private int currentLevel;

    // TODO REFACTOOOOOR PLEAAAASE
    public GameObject tower;
	public CapsuleCollider towerRange;
	public Unit enemy;
	public Weapon weapon;
	public GameObject enemy_object;
	public bool enemy_in;
	public List<Unit> enemies;
    public AudioClip shootSound;
    private AudioSource source;
    
    // Use this for initialization
    void Start ()
    {
        this.currentLevel = 0;

		// Collider of the tower
		towerRange = tower.GetComponent<CapsuleCollider> ();

		// Assignes the radius of the collider as the weapon attack range
		towerRange.radius = weapon.currentRange;

		// Fires every "cooldown of the weapon" seconds
		InvokeRepeating ("Attack", 0.0f, weapon.currentCooldown);

		// No enemy inside the range at the beginning
		enemy_in = false;

		// List of enemies when more than one enters the range
		enemies = new List<Unit> ();

		Debug.Log ("TOWER CREATED");
	}
    //USeed for sounds
    void Awake()
    {

        source = GetComponent<AudioSource>();

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
		Debug.Log ("TOWER UPGRADED, Power: " + this.weapon.currentDamage);
	}

	// To attack enemies
	public void Attack ()
	{
		// Checks if there is an enemy in the range
        // TODO This should be done inside its weapon
		if (enemy_in && enemy != null) {
			if (enemy.currentHealth > 1.0f) {
				enemy.currentHealth = enemy.currentHealth - weapon.currentDamage;
				Debug.Log ("Enemy's currentHealth: " + enemy.currentHealth);
                Awake();
                float vol = Random.Range(.5f, 1.0f);
                source.PlayOneShot(shootSound, vol);

            } else {
				enemy_object = enemy.gameObject;
				Destroy (enemy_object);
				Debug.Log ("Enemy dead");

				if (enemies.Count > 0) {
					Debug.Log ("Another enemy");
					enemy = enemies [0];
					enemies.Remove (enemy);
				} else {
					enemy_in = false;
				}
			}
		}
        //
	}

    // TODO should go inside Weapon
	void OnTriggerEnter (Collider col)
	{
		Debug.Log ("Collision");
		if (!enemy_in) {
			enemy_in = true;
			Debug.Log ("Enemy In");

			// Gets the script of the Collider that entered the radius of the tower
			enemy = col.gameObject.GetComponent<Unit> ();

		} else {
			// Adds enemy to attack to the queue
			enemies.Add (col.gameObject.GetComponent<Unit> ());
			Debug.Log ("Enemy in queue");
		}
			
	}
    //
}
