using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingWeapon : Weapon {
    private void Start()
    {
        currentDamage = baseDamage;
        currentRange = baseRange;
        currentCooldown = baseCooldown;

        // List of targets assigned to the weapon
        targets = new List<CanReceiveDamage>();

        // Get first projectile (or only one in Units case)
        this.proj_obj = projectiles[0];

        // TODO source_shoot assignation should go in Projectile
        source_shoot = GameObject.Find("Shoot Audio Source").GetComponent<AudioSource>();
        CancelInvoke("Attack");
    }

    public new void Upgrade()
    {
        currentDamage *= upgradeFactor;
        currentRange *= 2.0f;
    }
}
