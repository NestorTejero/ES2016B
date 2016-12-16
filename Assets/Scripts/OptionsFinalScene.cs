using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsFinalScene : MonoBehaviour
{
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
        SceneManager.LoadScene("MainMenu");
    }

    public void Reload()
    {
        SceneManager.LoadScene("LightCameraTest");
    }

    public void exit()
    {
        Application.Quit();
    }
}