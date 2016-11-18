using UnityEngine;
using System.Collections;

public interface CanReceiveDamage
{
	void ReceiveDamage (Projectile proj);
	GameObject getGameObject ();
}
