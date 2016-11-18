using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	// List of objects that we show when the game is paused
	GameObject[] pauseObjects;
	
	// Text field that will show the tooltip:
	public Text TooltipText;

	// We need this boolean to know if game is paused
	bool gamePaused;
	
	// Control of volume:
	public Slider volumeSlider;
	public Slider musicSlider;
	
	// Menu icons:
	public Image musicIcon; // This is the gameobject that shows the music volume sprite
	public Image effectsIcon; // This is the gameobject that shows the effects volume sprite
	// Sprites according to volume level:
	public Sprite soundFull;
	public Sprite soundMedium;
	public Sprite soundLow;
	public Sprite soundMuted;
	
	// We store the current volume before muting sound with icons
	private float lastMVolume; // Music volume
	private float lastEVolume; // Effects volume

	// Initialization
	void Start () {
		Time.timeScale = 1; // Game speed
		pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
		hidePaused();
		gamePaused = false;
		// Theese listeners help us to know if slider's values changed:
		volumeSlider.onValueChanged.AddListener (delegate {effectsChangeCheck ();});
		musicSlider.onValueChanged.AddListener (delegate {musicChangeCheck ();});
		
		lastMVolume = 1;
		lastEVolume = 1;
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
				Time.timeScale = 1;
				hidePaused();
			}
		}

	}
	
	// This function mutes or unmuted the effects volume, according to the current value
	public void effectsIconClick(){
		if (AudioListener.volume == 0){
			if (lastEVolume == 0){
				lastEVolume = 1;
			}
			AudioListener.volume = lastEVolume;
			volumeSlider.value = lastEVolume;
			setEffectsIcon();
		}
		else{
			float aux = AudioListener.volume;
			AudioListener.volume = 0;
			volumeSlider.value = 0;
			setEffectsIcon();
			lastEVolume = aux;
			
		}
	}
	
	// This function is called when we change the effects slider value
	public void effectsChangeCheck(){
		// We set the AudioListener volume according to the effects slider
		AudioListener.volume = volumeSlider.normalizedValue;
		lastEVolume = AudioListener.volume;
		
		setEffectsIcon();
		
	}
	
	// This function sets the correct icon volume
	private void setEffectsIcon(){
		// Different icon is shown according to the volume
		if (volumeSlider.value == 0){
			effectsIcon.sprite = soundMuted;
		}
		else if(volumeSlider.normalizedValue < 0.5f){
			effectsIcon.sprite = soundLow;
		}
		else if(volumeSlider.normalizedValue < 1.0f){
			effectsIcon.sprite = soundMedium;
		}
		else{
			effectsIcon.sprite = soundFull;
		}
	}
	
	// This function mutes or unmuted the music volume, according to the current value
	public void musicIconClick(){
		if (musicSlider.value == 0){
			if (lastMVolume == 0){
				lastMVolume = 1;
			}
			musicSlider.value = lastMVolume;
			setMusicIcon();
		}
		else{
			float aux = musicSlider.value;
			musicSlider.value = 0;
			setMusicIcon();
			lastMVolume = aux;
		}
	}
	
	// This function is called everytime we change the music slider value
	public void musicChangeCheck(){
		
		lastMVolume = musicSlider.value;
		setMusicIcon();
	}
	
	// This function sets the correct icon volume
	private void setMusicIcon(){
		// Different icon is shown according to the volume
		if (musicSlider.value == 0){
			musicIcon.sprite = soundMuted;
		}
		else if(musicSlider.normalizedValue < 0.5f){
			musicIcon.sprite = soundLow;
		}
		else if(musicSlider.normalizedValue < 1.0f){
			musicIcon.sprite = soundMedium;
		}
		else{
			musicIcon.sprite = soundFull;
		}
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
