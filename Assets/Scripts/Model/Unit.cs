using System;
using UnityEngine;

public class Unit : MonoBehaviour, CanReceiveDamage, HUDSubject
{
    public float baseHealth;
    public Transform goal;
    public float damage;

    private HealthComponent health;
    public float moveSpeed;
    public int purchaseCost;
    public int rewardCoins;
    public Weapon weapon;

    private GameObject model;
    private UnitAnimation animScript;

    private NavMeshAgent agent;

    public Texture normalTexture;
    public Texture damagedTexture;
    private GameObject textureModel;
    private SkinnedMeshRenderer skin;

    private AudioClip[] death;
    private AudioSource source_death;

    // Receive damage by weapon
    public void ReceiveDamage(float damage)
    {
        try
        {
            health.LoseHealth(damage);
            NotifyHUD();
            Debug.Log("UNIT " + name + " CURRENT_HEALTH: " + health.GetCurrentHealth());
        }
        catch (Exception)
        {
            NotifyHUD();
	    this.Die();
        }
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

    public void NotifyHUD()
    {
        var updateInfo = new HUDInfo
        {
            CurrentHealth = health.GetCurrentHealth(),
            TotalHealth = health.GetTotalHealth(),
            Damage = weapon.getCurrentDamage().ToString(),
            Range = weapon.getCurrentRange().ToString()
        };

        APIHUD.instance.notifyChange(this, updateInfo);
    }

    // Use this for initialization
    private void Start()
    {
        health = new HealthComponent(baseHealth);

        //NAVMESH DATA FOR PATHFINDING Unit movement towards the goal  
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
        agent.speed = moveSpeed;
        agent.acceleration = moveSpeed;
        agent.angularSpeed = 200f;

        //ANIMATION DATA(We search parent object for Model SubObject and use animation script for animating everything)
        model = this.transform.FindChild("Model").gameObject;
        //Debug.Log(gameObject.name  +gameObject.GetHashCode() + model.name + "FOUND");
        animScript = model.GetComponent<UnitAnimation>();

        //WEAPON SCRIPT DATA
        weapon = gameObject.GetComponent<Weapon>();
        damage = weapon.baseDamage;
        weapon.setAnimScript(animScript);

        //TEXTURE DATA
        //need to destroy material manualy when destroying object
        textureModel = model.transform.FindChild("UnitMesh").gameObject;
        Debug.Log(textureModel.name);
        skin = textureModel.GetComponent<SkinnedMeshRenderer>();
        skin.material.SetTexture("_MainTex", normalTexture);

        // Set sounds
        death = new[]
        {
            (AudioClip) Resources.Load("Sound/Effects/Death 1"),
            (AudioClip) Resources.Load("Sound/Effects/Death 2"),
            (AudioClip) Resources.Load("Sound/Effects/Death 3")
        };

        source_death = GameObject.Find("Death Audio Source").GetComponent<AudioSource>();

        Debug.Log("UNIT CREATED");
    }

    // If enemy enters the range of attack
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<Building>())
        {
            Debug.Log("Unit " + name + " Collision with Building");
            // Adds enemy to attack to the queue
            weapon.addTarget(col.gameObject.GetComponent<CanReceiveDamage>());
        }
    }

    // If enemy exits the range of attack
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<Building>())
            weapon.removeTarget(col.gameObject.GetComponent<CanReceiveDamage>());
    }

    public float GetDamage()
    {
        return damage;
    }

   /*
    * This funcion Kills Unit and Plays Necesary Sounds and Animations
    *
    * */
    public void Die()
    {

        //Stop VavMesh Agent From Moving Further
        agent.enabled = false;
        //Disable Colider to avoid colliding with projectiles when dead
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        model.GetComponent<CapsuleCollider>().enabled = false;
        GameController.instance.notifyDeath(this); // Tell controller I'm dead
        //PLAY DIE SOUND
        if (!source_death.isPlaying)
            source_death.PlayOneShot(death[UnityEngine.Random.Range(0, death.Length)], 0.5f);

        animScript.Die();

        Destroy(gameObject, 1.5f);

    }

    public void setSourceDeath(AudioSource death)
    {
        source_death = death;
    }
    //TODO Make Damaged texture apear when unit has <50% HP

}
