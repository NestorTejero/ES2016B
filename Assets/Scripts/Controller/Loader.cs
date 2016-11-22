using UnityEngine;

/**
 * Loads Singleton GameController in the beginning of the game
 */

public class Loader : MonoBehaviour
{
    public GameObject gameController;
	public GameObject MediumAI;
	public GameObject EasyAI;
	public GameObject HardAI;

    private void Awake()
    {
        //Check if a GameManager has already been assigned to static variable GameController.instance or if it's still null
		if (GameController.instance == null) 
			//Instantiate GameController prefab
			Instantiate (gameController);

	}

	private void Start(){
		if (PersistentValues.difficulty == 1) {
			Instantiate (EasyAI);
		} else if (PersistentValues.difficulty == 2) {
			Instantiate (EasyAI);
		} else if (PersistentValues.difficulty == 3) {
			Instantiate (HardAI);
		}

	}


}