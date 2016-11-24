using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int totalWaves;

    // Use this for initialization

    private void Start()
    {
        currentWave = 1;
        totalWaves = 2; //TODO: this number changes depending of AI level

        APIHUD.instance.setWave(currentWave.ToString());
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

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void notifyDeath(CanReceiveDamage dead)
    {
        if (dead is Building)
        {
            Debug.Log("GAME OVER Mate.");
            SceneManager.LoadScene("MainMenu");
        }
        else if (dead is Unit)
        {
            Debug.Log(((Unit) dead).name + " is dead.");
            GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().getMoney((Unit) dead);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void notifyWaveClear(AI ai)
    {
        currentWave += 1;
        Debug.Log("Wave CLEAR!");
        if (currentWave > totalWaves)
        {
            SceneManager.LoadScene("MainMenu");
        }
        ai.timeToUpgrade();

        APIHUD.instance.setWave(currentWave.ToString());
    }
}