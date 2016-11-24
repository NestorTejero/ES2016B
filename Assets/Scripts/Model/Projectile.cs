﻿using UnityEngine;

/**
 * Class that represents the projectile shot by a Weapon
 */

public class Projectile : MonoBehaviour
{
    private Rigidbody proj;
    private Vector3 target_position;

    private void Start()
    {
        proj = gameObject.GetComponentInChildren<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("------------------------------->Shoot");
        var velocity = Vector3.zero;
        proj.transform.position = Vector3.SmoothDamp(proj.transform.position, target_position, ref velocity,
            Time.deltaTime);
        //proj.transform.position = Vector3.Slerp(proj.transform.position, target_position, Time.deltaTime*2.0f);
    }

    public void Shoot(CanReceiveDamage target, float damage)
    {
        target_position = target.getGameObject().transform.position;
        target.ReceiveDamage(damage);
        Debug.Log("DAMAGE ENEMY");
    }
}