using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public int totalWaves;
    private int currentWave;
    private float minSoundVolume;
    private float maxSoundVolume;
    private float currentSoundVolume;
	// Use this for initialization
	void Start ()
	{
        this.currentWave = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

    void SetMinSoundVolume(float s)
    {
        this.minSoundVolume = s;
    }

    void SetMaxSoundVolume(float s)
    {
        this.maxSoundVolume = s;
    }

    void SetCurrentSoundVolume(float s)
    {
        this.currentSoundVolume = s;
    }
}
