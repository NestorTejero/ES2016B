using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public bool isQuitButton = false;
	public bool isHelpButton = false;
	public bool isOptionsButton = false;

	// Use this for initialization
	void Start () {
		//Should the cursor be visible?
		Cursor.visible = true;
		//The cursor will automatically be hidden, centered on view and made to never leave the view.
		Screen.lockCursor = false;
	}

	public void OnMouseEnter(){
		GetComponent<Renderer>().material.color = Color.black;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMouseExit(){
		GetComponent<Renderer>().material.color = Color.white;
	}

	public void OnMouseUpAsButton(){

		if(isQuitButton)
			Application.Quit();
		else if(isHelpButton)
			SceneManager.LoadScene(4); 
		else if(isOptionsButton)
			SceneManager.LoadScene(3); 
		else
			SceneManager.LoadScene(2); //Load the game (next scene)
	}
}
