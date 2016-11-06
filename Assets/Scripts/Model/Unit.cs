using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Unit : MonoBehaviour, CanReceiveDamage
{
    public float baseHealth;
    public float moveSpeed;
    public int costCoins;
    public int rewardCoins;
    public Transform goal;
    private Weapon weapon;

    private float totalHealth;
	private float currentHealth;
	
	// Tooltip field
	public Text TooltipText;
    

	// Use this for initialization
	void Start ()
	{
		this.currentHealth = this.baseHealth;
        this.totalHealth = this.baseHealth;

		// Unit movement towards the goal
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = this.goal.position;

	    this.weapon = this.gameObject.GetComponent<Weapon>();
		
		TooltipText = GameObject.Find("TooltipText").GetComponent<Text>();

        Debug.Log ("UNIT CREATED");
	}
	
	// Update is called once per frame
	void Update ()
	{

	}
	
	// This function is called when we put the mouse over the gameobject
	void OnMouseOver ()
    {
		// Change the text bellow to set a new tooltip message
		TooltipText.text = "Kill them with fire!";

    }
	
	// This function is called when we put the mouse out of the gameobject
	void OnMouseExit ()
    {
		TooltipText.text = "";

    }

	// Receive damage from a projectile (shot by weapon)
	public bool ReceiveDamage (Projectile proj)
	{
		this.currentHealth -= proj.getDamage();
		Debug.Log ("Unit " + this.name +" currentHealth: " + this.currentHealth);
		//Debug.Log("UNIT DAMAGED by HP: " + proj.getDamage());

		if (this.currentHealth <= 0.0f)
		{
			Destroy (this.gameObject);
			Debug.Log ("Unit " + this.name + " is dead");
			return true;
		} else {
			return false;
		}
	}

	// If enemy enters the range of attack
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.GetComponent<Building> ()) {
			Debug.Log ("Unit " + this.name + " Collision with Building");
			// Adds enemy to attack to the queue
			this.weapon.addTarget (col.gameObject.GetComponent<CanReceiveDamage> ());
		}
	}

	// If enemy exits the range of attack
	void OnTriggerExit (Collider col)
	{
		if (col.gameObject.GetComponent<Building> ()) {
			// Removes enemy to attack from the queue
			this.weapon.removeTarget (col.gameObject.GetComponent<CanReceiveDamage> ());
		}
	}

    void OnDestroy()
    {
        GameController.instance.notifyDeath(this); // Tell controller I'm dead
    }
}
