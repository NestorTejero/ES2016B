using UnityEngine;

/**
 * Singleton GameController class
 */

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private float currentSoundVolume;
    private int currentWave;
    private float maxSoundVolume;
    private float minSoundVolume;

    public Player playerAttacker;
    public Player playerDefender;

    public Shop shop;

    public int totalWaves;

    // Use this for initialization

    private void Start()
    {
        currentWave = 0;

        playerAttacker = new Player();
        playerAttacker.setLogic(new EasyAI()); // TODO choose when there'll be different difficulties

        playerDefender = new Player();
        playerDefender.setLogic(new HumanAI());

        shop = new Shop();
    }

    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    public void SetMinSoundVolume(float s)
    {
        minSoundVolume = s;
    }

    public void SetMaxSoundVolume(float s)
    {
        maxSoundVolume = s;
    }

    public void SetCurrentSoundVolume(float s)
    {
        currentSoundVolume = s;
    }

    public void notifyDeath(CanReceiveDamage dead)
    {
        if (dead is Building)
        {
            Debug.Log("GAME OVER Mate.");
            // TODO Insert losing logic here
        }
        else if (dead is Unit)
        {
            Debug.Log(((Unit) dead).name + " is dead.");
            playerDefender.notifyDeath((Unit) dead);
        }
    }
}