using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float baseCooldown;
    public float baseDamage;
    public float baseRange;

    private float currentCooldown;
    private float currentDamage;
    private float currentRange;

    private AudioClip[] death;
    public GameObject proj_obj; // Projectile prefab
    public GameObject proj_origin; // Projectile origin
    public AudioClip shootSound;
    private AudioSource source_shoot, source_death;
    private List<CanReceiveDamage> targets;
    public float upgradeFactor;

    private UnitAnimation animScript;
    // Use this for initialization
    private void Start()
    {
        currentDamage = baseDamage;
        currentRange = baseRange;
        currentCooldown = baseCooldown;

        // Collider of the tower attached to this script
        gameObject.GetComponentInChildren<CapsuleCollider>().radius = currentRange;

        // List of targets assigned to the weapon
        targets = new List<CanReceiveDamage>();


        // Call Attack every 'cooldown' seconds
        InvokeRepeating("Attack", 0.0f, currentCooldown);

        // Set sounds
        death = new[]
        {
            (AudioClip) Resources.Load("Sound/Effects/Death 1"),
            (AudioClip) Resources.Load("Sound/Effects/Death 2"),
            (AudioClip) Resources.Load("Sound/Effects/Death 3")
        };

        source_death = GameObject.Find("Death Audio Source").GetComponent<AudioSource>();
        source_shoot = GameObject.Find("Shoot Audio Source").GetComponent<AudioSource>();
        Debug.Log("WEAPON CREATED");


        
    }

    // Upgrade weapon features
    public void Upgrade()
    {
        currentDamage *= upgradeFactor;
    }

    // Get weapon's current damage
    public float getCurrentDamage()
    {
        return currentDamage;
    }

    public float getCurrentRange()
    {
        return currentRange;
    }

    // Add target to list
    public void addTarget(CanReceiveDamage target)
    {
        targets.Add(target);
        Debug.Log(gameObject.name + "-> Targets to attack :" + targets.Count);
    }

    // Remove target from list
    public void removeTarget(CanReceiveDamage target)
    {
        targets.Remove(target);
        // TODO Careful! This is not the moment when the enemy dies (it is just removed from the target list)
        // Play death sound
        if (!source_death.isPlaying)
            source_death.PlayOneShot(death[Random.Range(0, death.Length)], 0.5f);
        Debug.Log(gameObject.name + "-> Targets to attack :" + targets.Count);
    }

    // Get the available target to attack from the targets list
    public CanReceiveDamage getAvailableTarget()
    {
        // Checks if there is a target in the range
        while (targets.Count > 0)
        {
            // Get target to attack
            var target = targets[0];

            // Check if target is already dead
            if (target.Equals(null))
            {
                Debug.Log(gameObject.name + ": TARGET ALREADY DEAD");
                removeTarget(target);
            }
            else
            {
                Debug.Log(gameObject.name + ": TARGET AVAILABLE TO SHOOT");
                return target;
            }
        }
        Debug.Log(gameObject.name + ": NO TARGETS ON QUEUE");
        return null;
    }

    // Called to attack a target
    public void Attack()
    {
        var target = getAvailableTarget();

        if (target != null)
        {
            // Play shoot sound
            if (!source_shoot.isPlaying)
                source_shoot.PlayOneShot(shootSound);
            //Creates projectile with its properties and destroys it after 3 seconds
            var proj_clone =
                (GameObject)
                Instantiate(proj_obj, proj_origin.transform.position, proj_origin.transform.rotation);
            proj_clone.GetComponent<Projectile>().Shoot(target, currentDamage);
            Destroy(proj_clone, 3.0f);
        }
        //Animation data
        if (tag == "Unit")
        {
            animScript.Attack();
        }
    }

    public void setSourceDeath(AudioSource death)
    {
        source_death = death;
    }

    public void setSourceShoot(AudioSource shoot)
    {
        source_shoot = shoot;
    }
    public void setAnimScript(UnitAnimation ascript)
    {
        this.animScript = ascript;
    }
}