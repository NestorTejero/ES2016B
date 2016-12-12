using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;

    // Use this for initialization
    private void Start()
    {
        //Should the cursor be visible?
        Cursor.visible = true;
        //The cursor will automatically be hidden, centered on view and made to never leave the view.
        Screen.lockCursor = false;
    }

    public void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }

    public void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    public void goToGame()
    {
        StartCoroutine(playAndLoad(2));
    }

    public void goToOptions()
    {
        StartCoroutine(playAndLoad(3));
    }

    public void goToHelp()
    {
        StartCoroutine(playAndLoad(4));
    }

    public void exit()
    {
        Application.Quit();
    }

    public IEnumerator playAndLoad(int scene)
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene(scene); //Load the game (next scene)  
    }
}