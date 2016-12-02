using UnityEngine;

public class Unit : MonoBehaviour, CanReceiveDamage
{
    public float baseHealth;
    private float currentHealth;
    // TODO This shouldn't be public
    public float damage;
    public Transform goal;
    public float moveSpeed;
    public int purchaseCost;
    public int rewardCoins;

    private float totalHealth;
    public Weapon weapon;

    private GameObject model;
    private UnitAnimation animScript;

    private NavMeshAgent agent;

    public Texture normalTexture;
    public Texture damagedTexture;
    private GameObject textureModel;
    private SkinnedMeshRenderer skin;
    // Receive damage by weapon
    public void ReceiveDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Unit " + name + " currentHealth: " + currentHealth);
        //Debug.Log("UNIT DAMAGED by HP: " + proj.getDamage());

        if (APIHUD.instance.getGameObjectSelected() == gameObject)
            APIHUD.instance.setHealth(currentHealth, totalHealth);

        if (currentHealth <= 0.0f)
            this.Die();


    }

    public GameObject getGameObject()
    {
        return gameObject;
    }


    // Use this for initialization
    private void Start()
    {
        currentHealth = baseHealth;
        totalHealth = baseHealth;

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

    public float getTotalHealth()
    {
        return totalHealth;
    }

    public float getCurrentHealth()
    {
        return currentHealth;
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
        //PLAY DIE SOUND HERE


        animScript.Die();

        Destroy(gameObject, 1.5f);

    }
    //TODO Make Damaged texture apear when unit has <50% HP

}