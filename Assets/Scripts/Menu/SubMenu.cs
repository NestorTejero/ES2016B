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


    // Use this for initialization
    void Start () {
		
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

	public void OnEasy(){
		PersistentValues.difficulty = 1;
		StartCoroutine(playAndLoad(5));
	}

	public void OnMedium(){
		PersistentValues.difficulty = 2;
		StartCoroutine(playAndLoad(5));
	}

	public void OnHard(){
		PersistentValues.difficulty = 3;
		StartCoroutine(playAndLoad(5));
	}

	public void OnBack(){
		StartCoroutine(playAndLoad(0));
	}
		
    public IEnumerator playAndLoad(int scene)
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene(scene); //Load the game (next scene)  
    }
}
