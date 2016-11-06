using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour, CanUpgrade
{
    public int upgradeCost;
    private Weapon weapon;
	private int currentLevel;
	
	// Tooltip field
	public Text TooltipText;
    
    // Use this for initialization
    void Start ()
    {
        this.weapon = this.gameObject.GetComponent<Weapon>();
        this.currentLevel = 0;
		
		TooltipText = GameObject.Find("TooltipText").GetComponent<Text>();
		
		Debug.Log ("TOWER CREATED");
	}
		
    // Update is called once per frame
    void Update ()
	{

	}
	
	// This function is called when we put the mouse over the gameobject
	void OnMouseOver ()
    {
		// Change the text bellow to set a new tooltip message
		TooltipText.text = "Protect this building!";

    }
	
	// This function is called when we put the mouse out of the gameobject
	void OnMouseExit ()
    {
		TooltipText.text = "";

    }
	
	// To upgrade when there are enough coins
	public void Upgrade ()
	{
		this.weapon.Upgrade();
	    this.currentLevel++;
		Debug.Log ("TOWER UPGRADED, Power: " + this.weapon.getCurrentDamage());
	}

	// If enemy enters the range of attack
	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.tag == "Unit") {
			Debug.Log ("Tower " + this.name + " Collision with Unit");

			// Adds enemy to attack to the queue
			this.weapon.addTarget (col.gameObject.GetComponent<CanReceiveDamage> ());
		}
	}


	// If enemy exits the range of attack
	void OnTriggerExit (Collider col){
		if (col.gameObject.tag == "Unit") {

			// Removes enemy to attack from the queue
			this.weapon.removeTarget (col.gameObject.GetComponent<CanReceiveDamage> ());
		}
	}
}
