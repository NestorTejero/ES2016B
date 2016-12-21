using UnityEngine;

/**
 * Class that represents the projectile shot by a Weapon
 */

public class Projectile : MonoBehaviour
{
    private float damage;
    private GameObject proj;
    public AudioClip shootSound;
    public float speed;

    private CanReceiveDamage target;
    private Vector3 target_position;

    private void Start()
    {
        proj = gameObject;
        // Rotate projectile to face the target.
        proj.transform.rotation = Quaternion.LookRotation(target_position - proj.transform.position);
    }

    // Update is called once per frame
    private void Update()
    {
		if (this.gameObject.tag == "Laser") {
			proj.transform.position = Vector3.Lerp (proj.transform.position, this.target_position, Time.deltaTime * this.speed);
		} else {
			proj.transform.position = Vector3.Slerp (proj.transform.position, this.target_position, Time.deltaTime * this.speed);
		}
    }

    public void Shoot(CanReceiveDamage target, float damage)
    {
        this.target = target;
        this.target_position = target.getGameObject().transform.position;
        this.damage = damage;
    }

    // For empty shot without colliding with anyone
    public void Shoot(Vector3 targetPosition)
    {
        target = null;
        target_position = targetPosition;
    }

    // If enemy enters the range of attack
    private void OnCollisionEnter(Collision col)
    {
        if ((target != null) && !target.Equals(null))
        {
            var target_tag = target.getGameObject().tag;
            var col_tag = col.collider.gameObject.tag;

			if (((col_tag == "Enemy") && (target_tag == "Unit")) || ((col_tag == "Building") && (target_tag == "Building"))) {
				target.ReceiveDamage (damage);
			}
        }
    }
}