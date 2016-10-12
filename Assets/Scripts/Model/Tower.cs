using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour, Upgradeable, CanAttack
{
    Weapon w;    
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
        this.w.Upgrade();
    }

	// To attack enemies
	public void Attack ()
	{

	}
}
