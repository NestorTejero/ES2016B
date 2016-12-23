using UnityEngine;

public class MusicController : MonoBehaviour
{
// Initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void Awake()
    {
        var music = GetComponent<AudioSource>();
        music.ignoreListenerVolume = true; // We make the audio source ignore the audio listener
        music.volume = 0.5f;
    }
}