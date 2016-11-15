using UnityEngine;
using System.Collections;

public interface CanReceiveDamage
{
	bool ReceiveDamage (Projectile proj);
	bool isDead ();
}
