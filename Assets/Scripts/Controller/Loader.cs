using UnityEngine;

/**
 * Loads Singleton GameController in the beginning of the game
 */

public class Loader : MonoBehaviour
{
    public GameObject gameController;

    private void Awake()
    {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        if (GameController.instance == null)

            //Instantiate gameManager prefab
            Instantiate(gameController);
    }
}