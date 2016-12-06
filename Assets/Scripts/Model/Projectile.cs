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
	private float angle;
	private float gravity;
	private float elapse_time;

    private void Start()
    {
        //this.proj = gameObject.GetComponentInChildren<Rigidbody>();
		this.proj = this.gameObject;
		this.angle = 20.0f;
		this.gravity = 9.8f;
		this.elapse_time = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("------------------------------->Shoot");

		// Calculate distance to target
		float distance = Vector3.Distance(this.proj.transform.position, this.target_position);

		/*
		// Calculate the velocity needed to throw the object to the target at specified angle.
		float velocity = distance / (Mathf.Sin(2 * angle * Mathf.Deg2Rad) / gravity);

		// Extract the X  Y componenent of the velocity
		float Vx = Mathf.Sqrt(velocity) * Mathf.Cos(angle * Mathf.Deg2Rad);
		float Vy = Mathf.Sqrt(velocity) * Mathf.Sin(angle * Mathf.Deg2Rad);
		*/

		// Extract the X  Y componenent of the velocity
		float Vx = Mathf.Sqrt(this.speed) * Mathf.Cos(angle * Mathf.Deg2Rad);
		float Vy = Mathf.Sqrt(this.speed) * Mathf.Sin(angle * Mathf.Deg2Rad);

		// Rotate projectile to face the target.
		this.proj.transform.rotation = Quaternion.LookRotation(this.target_position - this.proj.transform.position);

		this.proj.transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

		elapse_time += Time.deltaTime;
	}

    public void Shoot(CanReceiveDamage target, float damage)
    {
		this.target = target;
		this.target_position = target.getGameObject().transform.position;
		this.damage = damage;
    }

	// If enemy enters the range of attack
	private void OnTriggerEnter(Collider col)
	{
		string tag = this.target.getGameObject ().tag;
		GameObject collider = col.gameObject;

		if (collider.GetComponentInParent<Unit>() && tag == "Enemy")
		{
			Debug.Log("--------------------------------------------------------------- Projectile collision with Unit");
			this.target.ReceiveDamage(damage);
		}
		if(collider.GetComponent<Building>() && tag == "Building")
		{
			Debug.Log("--------------------------------------------------------------- Projectile collision with Building");
			this.target.ReceiveDamage(damage);
		}	

		/*		
		if ((collider.GetComponentInParent<Unit>() && tag == "Enemy") || (collider.GetComponent<Building>() && tag == "Building"))
		{
			Debug.Log("------------------------------ Projectile collision with Target");
			this.target.ReceiveDamage(damage);
		}
		*/
	}
		
}