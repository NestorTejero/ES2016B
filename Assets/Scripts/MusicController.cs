using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

// Initialization
	private void Start(){
		
	}

    // Update is called once per frame
    private void Update(){
		
	}

	void Awake () {
		AudioSource music = GetComponent<AudioSource>();
		music.ignoreListenerVolume = true; // We make the audio source ignore the audio listener
		music.volume = 0.5f;
	}
	
}
