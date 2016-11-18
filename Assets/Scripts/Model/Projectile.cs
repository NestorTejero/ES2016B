using UnityEngine;
using System.Collections;

/**
 * Class that represents the projectile shot by a Weapon
 */
public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
	private Rigidbody proj;
    private CanReceiveDamage target;
	private float damage;
	private bool attack;

	void Start() {
		this.attack = false;
		this.proj = this.gameObject.GetComponent<Rigidbody>();
	}
		

	// Update is called once per frame
	void Update () {
		if(this.attack){
			Debug.Log ("------------------------------->Shoot");
			GameObject target_obj = this.target.getGameObject ();
			Vector3 velocity = Vector3.zero;
			proj.transform.position = Vector3.SmoothDamp(proj.transform.position, target_obj.transform.position, ref velocity, Time.deltaTime);
		}
	}

	public void Properties(CanReceiveDamage target, float damage){
		Debug.Log ("PROPERTIES");
		this.target = target;
		this.damage = damage;
		this.attack = true;
	}

	// Shoot the projectile to the target
	public void Shoot(){
		Debug.Log ("ATTACK");
		//proj_clone.velocity = (target_obj.transform.position - proj.transform.position).normalized * this.projectileSpeed;
		//proj_clone.transform.position = Vector3.MoveTowards (proj_clone.transform.position, target_obj.transform.position, Time.deltaTime*this.projectileSpeed);
		this.target.ReceiveDamage (this);
		//this.attack = false;
	}

	// Get the projectile damage (defined by weapon)
	public float getDamage(){
		return this.damage;
	}
}
