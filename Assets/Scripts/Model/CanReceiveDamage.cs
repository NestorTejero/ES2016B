using UnityEngine;

public interface CanReceiveDamage
{
    void ReceiveDamage(float damage);
    GameObject getGameObject();
}