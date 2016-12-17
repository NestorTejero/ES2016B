using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubMenu : MonoBehaviour
{
    public AudioSource audioSource;

    public bool isEasyButton = false;
    public bool isHardButton = false;
    public bool isMediumButton = false;
    public bool isReturnButton = false;

    public GameObject menu;

    // Use this for initialization
    private void Start()
    {
        float width = Screen.width;
        float height = Screen.height;

        float aux1, aux2;
        aux1 = width/2.0f;
        aux2 = height/2.0f;

        menu.transform.position = new Vector3(aux1, aux2, 1);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }

    public void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }


    public void OnEasy()
    {
        PersistentValues.difficulty = 1;
        StartCoroutine(playAndLoad(5));
    }

    public void OnMedium()
    {
        PersistentValues.difficulty = 2;
        StartCoroutine(playAndLoad(5));
    }

    public void OnHard()
    {
        PersistentValues.difficulty = 3;
        StartCoroutine(playAndLoad(5));
    }

    public void OnBack()
    {
        StartCoroutine(playAndLoad(0));
    }

    public IEnumerator playAndLoad(int scene)
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene(scene); //Load the game (next scene)  
    }
}