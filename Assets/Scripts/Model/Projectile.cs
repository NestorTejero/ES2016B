using UnityEngine;
using System.Collections;

/**
 * Class that represents the projectile shot by a Weapon
 */
public class Projectile : MonoBehaviour
{
	private Rigidbody proj;
	private CanReceiveDamage target;
	private float damage;
	private Vector3 target_position;
	private bool attack;

	void Start() {
		this.proj = this.gameObject.GetComponentInChildren<Rigidbody>();
		this.target_position = Vector3.zero;
		this.attack = true;
		Debug.Log ("PROJECTILE CREATED");
	}
		

	// Update is called once per frame
	void Update () {
		if (this.attack) {
			Debug.Log ("------------------------------->Shoot");
			target_position = this.target.getGameObject ().transform.position;
			this.target.ReceiveDamage (damage);
			this.attack = false;
		}

		Vector3 velocity = Vector3.zero;
		proj.transform.position = Vector3.SmoothDamp(proj.transform.position, target_position, ref velocity, Time.deltaTime);
	}

	public void Properties(CanReceiveDamage target, float damage){
		this.target = target;
		this.damage = damage;
	}
}
