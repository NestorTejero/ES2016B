using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {
	
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

	public void goToGame(){
		SceneManager.LoadScene(2); //Load the game (next scene)
	}

	public void goToOptions(){
		SceneManager.LoadScene(3); 
	}

	public void goToHelp(){
		SceneManager.LoadScene(4); 
	}

	public void exit(){
		Application.Quit();
	}
}
