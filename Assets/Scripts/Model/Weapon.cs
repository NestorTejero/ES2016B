using UnityEngine;
using System.Collections;
using System;

public class Weapon : MonoBehaviour, Upgradeable
{
    public float cooldown;
    public float power;
    public float range;

    public float upgradeCooldownFactor;
    public float upgradePowerFactor;
    public float upgradeRangeFactor;

    // Use this for initialization
    void Start ()
	{

    }

	// Update is called once per frame
	void Update ()
	{

	}

    public void Upgrade()
    {
        this.cooldown *= upgradeCooldownFactor;
        this.power *= upgradePowerFactor;
        this.range *= upgradeRangeFactor;
    }
}
