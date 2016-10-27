using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour, CanUpgrade, CanRepair, CanReceiveDamage
{
    private float currentLevel;
    public float baseHealth;
    private float totalHealth;
    public float currentHealth; // TODO Change to private

	public float upgradeFactor;
    public float upgradeCost;
	public float repairQuantity;
    public float repairCost;

    private float timeToWin; // TODO move this to Controller

	// Use this for initialization
	void Start ()
	{
	    this.currentLevel = 0;

		this.currentHealth = this.baseHealth;
	    this.totalHealth = this.baseHealth;

        this.timeToWin = 1000.0f; // TODO move this to controller

        Debug.Log ("BUILDING CREATED with HP: " + this.baseHealth);
	}

	// Update is called once per frame
	void Update ()
	{
        // TODO move this to controller
        if (this.timeToWin <= 0.0f)
        {
            Application.LoadLevel("MainMenu");
        }
        this.timeToWin -= Time.deltaTime * 1;
        //
    }

	// To upgrade when there are enough coins
	public void Upgrade ()
	{
		this.totalHealth *= this.upgradeFactor;
		this.currentHealth *= this.upgradeFactor;
	    this.currentLevel++;
		Debug.Log ("BUILDING UPGRADED, now it has HP: " + this.totalHealth);
	}

	public void Repair ()
	{
		this.currentHealth += this.repairQuantity;
		if (this.currentHealth > this.totalHealth) {
			this.currentHealth = this.totalHealth;
		}
		Debug.Log ("BUILDING REPAIRED, HP: " + this.currentHealth);
	}

	public void ReceiveDamage (Weapon wep)
	{
		this.currentHealth -= wep.getCurrentDamage();
        // TODO should be moved to Controller
        if (this.currentHealth <= 0.0) {
            Application.LoadLevel("MainMenu");
		}
		Debug.Log ("BUILDING DAMAGED by HP: " + wep.getCurrentDamage());
        //
	}
}
