using UnityEngine;

/**
 * Loads Singleton GameController in the beginning of the game
 */

public class Loader : MonoBehaviour
{
    public GameObject gameController;
	public GameObject MediumAI;

    private void Awake()
    {
        //Check if a GameManager has already been assigned to static variable GameController.instance or if it's still null
        if (GameController.instance == null)

            //Instantiate GameController prefab
            Instantiate(gameController);
			
			//Instantiate(MediumAI);
			//MediumAI = (GameObject)Instantiate (Resources.Load ("Prefabs/MediumAI"));
		 //MediumAI = Instantiate(Resources.Load("Prefabs/Players/EasyAI")) as GameObject;
    }


}