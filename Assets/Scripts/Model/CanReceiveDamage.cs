using UnityEngine;
using System.Collections;

public interface CanReceiveDamage
{
	void ReceiveDamage (float damage);
	GameObject getGameObject ();
}
