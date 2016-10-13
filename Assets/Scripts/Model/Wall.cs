using UnityEngine;
using System.Collections;
using System;

public class Wall : MonoBehaviour, Upgradeable, Repairable
{
    public float health;
    public float upgradeHealthFactor;
    public float repairHealthQuantity;

    // Use this for initialization
    void Start ()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {

    }

    // To upgrade when there are enough coins
    public void Upgrade ()
    {
        this.health *= this.upgradeHealthFactor;
    }

    public void Repair ()
    {
        this.health += this.repairHealthQuantity;
    }
}
