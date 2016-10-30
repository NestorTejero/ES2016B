using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour
{
    public GameObject gameController;
    void Awake()
    {
        //Check if a GameManager has already been assigned to static variable GameManager.instance or if it's still null
        if (GameController.instance == null)

            //Instantiate gameManager prefab
            Instantiate(gameController);
    }
}
