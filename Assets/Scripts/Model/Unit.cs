using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour, CanAttack, CanReceiveDamage
{
	public Transform goal;
	public float maxHealth;
	public float health; // TODO change to private
    public Weapon weapon;
    public GameObject objectTarget;
    private Building target;
	// Use this for initialization
	void Start ()
	{
		health = maxHealth;
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = goal.position;
        target = objectTarget.gameObject.GetComponent<Building>();

        InvokeRepeating("Attack", 0.0f, 1.0f);
        Debug.Log ("UNIT CREATED");
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	// To attack wall when near enough
	public void Attack ()
	{
        target.health -= 1.0f;
        Debug.Log("Unit: " + this.name + "attaks" + target.name);
        Debug.Log("Target Health" + target.health);
    }

	// To upgrade when there are enough coins
	public void ReceiveDamage (Weapon wep)
	{
		this.health -= wep.power;
		if (this.health <= 0.0f) {
			// TODO implement death and stuff here
				// The logic that destroys the enemy shouldn't go inside Tower	
		}
		Debug.Log ("UNIT DAMAGED by HP: " + wep.power);
	}
}
