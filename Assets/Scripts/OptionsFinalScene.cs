using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class OptionsFinalScene : MonoBehaviour
{
    public AudioSource audioSource;
    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void loadMainMenu()
    {
        StartCoroutine(playAndLoad("MainMenu"));
        //SceneManager.LoadScene("MainMenu");
    }

    public void Reload()
    {
        StartCoroutine(playAndLoad("LightCameraTest"));
        //SceneManager.LoadScene();
    }

    public void exit()
    {
        Application.Quit();
    }

    public IEnumerator playAndLoad(string scene)
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene(scene); //Load the game (next scene)  
    }
}