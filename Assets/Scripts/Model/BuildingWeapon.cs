using System.Collections.Generic;
using UnityEngine;

public class BuildingWeapon : Weapon
{
    private void Start()
    {
        currentDamage = baseDamage;
        currentRange = baseRange;
        currentCooldown = baseCooldown;

        // List of targets assigned to the weapon
        targets = new List<CanReceiveDamage>();

        // Get first projectile (or only one in Units case)
        proj_obj = projectiles[0];

        // TODO source_shoot assignation should go in Projectile
        source_shoot = GameObject.Find("Shoot Audio Source").GetComponent<AudioSource>();
        CancelInvoke("Attack");
    }

    public new void Upgrade()
    {
        currentDamage *= upgradeFactor;
        currentRange *= 2.0f;
    }

    public void ShootAt(Vector3 position)
    {
        if (!source_shoot.isPlaying)
            source_shoot.PlayOneShot(shootSound);

        //Creates projectile with its properties and destroys it after 3 seconds
        var proj_clone =
            (GameObject) Instantiate(proj_obj, proj_origin.transform.position, proj_origin.transform.rotation);
        proj_clone.GetComponent<Projectile>().Shoot(position);
        Destroy(proj_clone, 3.0f);
    }
}