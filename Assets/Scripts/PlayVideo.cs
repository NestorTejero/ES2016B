using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(AudioSource))]

public class PlayVideo : MonoBehaviour {

	public MovieTexture movieLose;
	public MovieTexture movieWin;
	private AudioSource audio;

	// Use this for initialization
	void Start () {
		switch (PersistentValues.victory)
		{
		case 0:
			Debug.Log("GameOver");
			GetComponent<RawImage>().texture = movieLose as MovieTexture;
			audio = GetComponent<AudioSource>();
			audio.clip = movieLose.audioClip;
			movieLose.Play();
			audio.Play();
			break;
		case 1:
			Debug.Log("Win");
			GetComponent<RawImage>().texture = movieWin as MovieTexture;
			audio = GetComponent<AudioSource>();
			audio.clip = movieWin.audioClip;
			movieWin.Play();
			audio.Play();
			break;
		}


	}

	// Update is called once per frame
	void Update () {
		/*if(Input.GetKeyDown(KeyCode.Space) && movie.isPlaying){
			movie.Pause();
		}
		else if(Input.GetKeyDown(KeyCode.Space) && !movie.isPlaying){
			movie.Play();
		}*/
	}



	//loads inputted level
	public void loadMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
	public void Reload()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
	public void exit()
	{
		Application.Quit();
	}
}
