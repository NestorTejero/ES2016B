using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;


public class SubMenu : MonoBehaviour {

	public bool isEasyButton = false;
	public bool isMediumButton = false;
	public bool isHardButton = false;
	public bool isReturnButton = false;
    public AudioSource audioSource;

	// Control of volume:
	public Slider volumeSlider = null;
	public Slider musicSlider = null;

	public Image musicIcon;
	public Image effectsIcon;

	public Sprite soundFull;
	public Sprite soundMedium;
	public Sprite soundLow;
	public Sprite soundMuted;

	private float lastMVolume; // Music volume
	private float lastEVolume; // Effects volume

    // Use this for initialization
    void Start () {
		//Should the cursor be visible?
		Cursor.visible = true;
		//The cursor will automatically be hidden, centered on view and made to never leave the view.
		Screen.lockCursor = false;
		// Sliders of Sound
		volumeSlider.onValueChanged.AddListener (delegate {effectsChangeCheck ();});
		musicSlider.onValueChanged.AddListener (delegate {musicChangeCheck ();});
		lastMVolume = 1;
		lastEVolume = 1;
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
            StartCoroutine(playAndLoad(5));
        else if(isMediumButton)
            StartCoroutine(playAndLoad(5));
        else if(isHardButton)
            StartCoroutine(playAndLoad(5));
        else if(isReturnButton)
            StartCoroutine(playAndLoad(0));
		else
			Application.Quit(); //Load the game (next scene)
	}
    public IEnumerator playAndLoad(int scene)
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene(scene); //Load the game (next scene)  
    }


	// Changes the effects sound
	public void effectsChangeCheck(){
		AudioListener.volume = volumeSlider.normalizedValue;
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

	// Changes the music sound
	public void musicChangeCheck(){
		AudioListener.volume = volumeSlider.normalizedValue;
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



	public void effectsIconClick(){
		if (AudioListener.volume == 0){
			if (lastEVolume == 0){
				lastEVolume = 1;
			}
			AudioListener.volume = lastEVolume;
			volumeSlider.value = lastEVolume;
			effectsChangeCheck();
		}
		else{
			float aux = AudioListener.volume;
			AudioListener.volume = 0;
			volumeSlider.value = 0;
			effectsChangeCheck();
			lastEVolume = aux;

		}
	}

	public void musicIconClick(){
		if (musicSlider.value == 0){
			if (lastMVolume == 0){
				lastMVolume = 1;
			}
			musicSlider.value = lastMVolume;
			musicChangeCheck();
		}
		else{
			float aux = musicSlider.value;
			musicSlider.value = 0;
			musicChangeCheck();
			lastMVolume = aux;
		}
	}
}
