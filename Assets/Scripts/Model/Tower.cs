using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour, CanUpgrade
{
	public Weapon weapon;
	private int currentLevel;
    
    // Use this for initialization
    void Start ()
    {
        this.currentLevel = 0;
		Debug.Log ("TOWER CREATED");
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
		Debug.Log ("TOWER UPGRADED, Power: " + this.weapon.getCurrentDamage());
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.GetComponent<Unit> ()) {
			Debug.Log ("Tower Collision with Unit");

			// Adds enemy to attack to the queue
			this.weapon.addTarget (col.gameObject.GetComponent<CanReceiveDamage> ());
		}
	}
}
