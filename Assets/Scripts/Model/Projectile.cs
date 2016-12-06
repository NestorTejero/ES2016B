using UnityEngine;

/**
 * Class that represents the projectile shot by a Weapon
 */

public class Projectile : MonoBehaviour
{
	public float speed;
	public AudioClip shootSound;

	private CanReceiveDamage target;
	private GameObject proj;
    private Vector3 target_position;
	private float damage;

    private void Start()
    {
		this.proj = this.gameObject;
		// Rotate projectile to face the target.
		this.proj.transform.rotation = Quaternion.LookRotation(this.target_position - this.proj.transform.position);
    }

    // Update is called once per frame
    private void Update()
    {
		this.proj.transform.position = Vector3.Slerp (this.proj.transform.position, this.target_position, Time.deltaTime*this.speed);
	}

    public void Shoot(CanReceiveDamage target, float damage)
    {
		this.target = target;
		this.target_position = target.getGameObject().transform.position;
		this.damage = damage;
    }

	// If enemy enters the range of attack
	private void OnCollisionEnter(Collision col)
	{
		if (!target.Equals (null)) {
			string target_tag = this.target.getGameObject ().tag;
			string col_tag = col.collider.gameObject.tag;

			if ((col_tag == "Enemy" && target_tag == "Unit") || (col_tag = "Building" && target_tag == "Building")) {
				this.target.ReceiveDamage (damage);
			}
		}
		
	}
		
}