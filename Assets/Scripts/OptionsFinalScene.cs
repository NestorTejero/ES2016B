using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsFinalScene : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
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