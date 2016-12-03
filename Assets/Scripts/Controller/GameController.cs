using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Singleton GameController class
 */

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private int currentWave;

    public int totalWaves;

    // Use this for initialization

    private void Start()
    {
        currentWave = 1;

        switch (PersistentValues.difficulty)
        {
            case 1:
                APIHUD.instance.setDifficulty("Easy");
                break;
            case 2:
                APIHUD.instance.setDifficulty("Medium");
                break;
            case 3:
                APIHUD.instance.setDifficulty("Hard");
                break;
        }

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

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameController.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
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
            SceneManager.LoadScene("MainMenu");
        ai.ChangeWave();

        APIHUD.instance.setWave(currentWave.ToString());

        GameObject.FindGameObjectWithTag("Human").GetComponent<Player>().ChangeWave();
    }
}