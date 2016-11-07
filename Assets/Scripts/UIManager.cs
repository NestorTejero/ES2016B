using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	GameObject[] pauseObjects;
	
	// Text field that will show the tooltip:
	public Text TooltipText;

	//we need this boolean to know if game is paused
	bool gamePaused;
	
	// Control of volume:
	public AudioListener audioListener;
	public Slider volumeSlider;


	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		hidePaused();
		gamePaused = false;
		TooltipText = GameObject.Find("TooltipText").GetComponent<Text>();
		
		GameObject temp = GameObject.Find("EffectsSlider");
		volumeSlider = temp.GetComponent<Slider>();
		volumeSlider.onValueChanged.AddListener (delegate {ValueChangeCheck ();});
		GameObject tempo = GameObject.Find("Main Camera(RTS)");
		audioListener = tempo.GetComponent<AudioListener>();
	}

	// Update is called once per frame
	void Update () {

		//uses the p button to pause and unpause the game
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			TooltipText.text = "";
			if(!gamePaused)
			{
				Time.timeScale = 0;
				showPaused();
				gamePaused = !gamePaused;
			} else if (gamePaused){
				Debug.Log ("high");
				Time.timeScale = 1;
				hidePaused();
			}
		}

	}
	
	// Changes the effects sound
	public void ValueChangeCheck()
	{
		//audioListener.volume = volumeSlider.value;
	}


	//Reloads the Level
	public void Reload(){
		Application.LoadLevel(Application.loadedLevel);
	}

	//controls the pausing of the scene
	public void pauseControl(){
		Time.timeScale = 1;
		hidePaused();
	}

	//shows objects with ShowOnPause tag
	public void showPaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(true);
		}
	}

	//hides objects with ShowOnPause tag
	public void hidePaused(){
		foreach(GameObject g in pauseObjects){
			g.SetActive(false);
		}
		Time.timeScale = 1;
		gamePaused = !gamePaused; // we need to change de boolean here
	}

	//loads inputted level
	public void LoadLevel(string level){
		Application.LoadLevel(level);
	}

}
