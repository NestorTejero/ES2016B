using UnityEngine;

/**
 * Class that represents the projectile shot by a Weapon
 */

public class Projectile : MonoBehaviour
{
	public float speed;
	private CanReceiveDamage target;
    private Rigidbody proj;
    private Vector3 target_position;
	private float damage;

    private void Start()
    {
        proj = gameObject.GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("------------------------------->Shoot");
        var velocity = Vector3.zero;
		proj.transform.position = Vector3.SmoothDamp(proj.transform.position, this.target_position, ref velocity, Time.deltaTime*this.speed);
        //proj.transform.position = Vector3.Slerp(proj.transform.position, target_position, Time.deltaTime*2.0f);
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
		//Debug.Log("Projectile collision with Unit/Building");
		//this.target.ReceiveDamage(damage);
		if (col.gameObject.GetComponent<Unit>() || col.gameObject.GetComponent<Building>())
		{
			Debug.Log("Projectile collision with Unit/Building");
			this.target.ReceiveDamage(damage);
		}
	}

}