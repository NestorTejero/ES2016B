using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour, CanReceiveDamage
{
	public float baseHealth;
    public float moveSpeed;
    public int costCoins;
    public int rewardCoins;

	private float totalHealth;
	private float currentHealth;

    public Transform goal;
    public Weapon weapon;
    private Building target;

	// Use this for initialization
	void Start ()
	{
		this.currentHealth = this.baseHealth;
        this.totalHealth = this.baseHealth;

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.position;
        target = gameObject.GetComponent<Building>();

		/////////////////////////
		//weapon.setTarget(target);

        Debug.Log ("UNIT CREATED");
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void ReceiveDamage (Weapon wep)
	{
		this.currentHealth -= wep.getCurrentDamage();
		Debug.Log ("Target's currentHealth: " + this.currentHealth);

		if (this.currentHealth <= 0.0f) {
			Destroy (this.gameObject);
			Debug.Log ("Enemy is dead");
		}

		Debug.Log("UNIT DAMAGED by HP: " + wep.getCurrentDamage());
	}

}
