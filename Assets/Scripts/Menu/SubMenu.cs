using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SubMenu : MonoBehaviour {

	public bool isEasyButton = false;
	public bool isMediumButton = false;
	public bool isHardButton = false;
	public bool isReturnButton = false;


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

		if(isEasyButton)
			Application.Quit();
		else if(isMediumButton)
			SceneManager.LoadScene(1); 
		else if(isHardButton)
			SceneManager.LoadScene(1); 
		else if(isReturnButton)
			SceneManager.LoadScene(0); 
		else
			SceneManager.LoadScene(1); //Load the game (next scene)
	}
}
