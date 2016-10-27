using UnityEngine;
using System.Collections;

/**
 * Class that represents the projectile shot by a Weapon
 */
public class Projectile : MonoBehaviour
{
    private float projectileSpeed;
    private CanReceiveDamage target;

	// Use this for initialization
    public Projectile(float projSpeed, CanReceiveDamage target)
    {
        this.projectileSpeed = projSpeed;
        this.target = target;
    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
