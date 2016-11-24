using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image effectsIcon; // This is the gameobject that shows the effects volume sprite

    // We need this boolean to know if game is paused
    private bool gamePaused;
    private float lastEVolume; // Effects volume

    // We store the current volume before muting sound with icons
    private float lastMVolume; // Music volume

    // Menu icons:
    public Image musicIcon; // This is the gameobject that shows the music volume sprite
    public Slider musicSlider;
    // List of objects that we show when the game is paused
    private GameObject[] pauseObjects;
    // Sprites according to volume level:
    public Sprite soundFull;
    public Sprite soundLow;
    public Sprite soundMedium;
    public Sprite soundMuted;

    // Text field that will show the tooltip:
    public Text TooltipText;

    // Control of volume:
    public Slider volumeSlider;

    // Initialization
    private void Start()
    {
        Time.timeScale = 1; // Game speed
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
        gamePaused = false;
        // Theese listeners help us to know if slider's values changed:
        volumeSlider.onValueChanged.AddListener(delegate { effectsChangeCheck(); });
        musicSlider.onValueChanged.AddListener(delegate { musicChangeCheck(); });

        volumeSlider.value = PersistentValues.effectsVolume;
        musicSlider.value = PersistentValues.musicVolume;
        lastMVolume = PersistentValues.musicLastVolume;
        lastEVolume = PersistentValues.effectsLastVolume;
    }

    // Update is called once per frame
    private void Update()
    {
        //uses the p button to pause and unpause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TooltipText.text = "";
            if (!gamePaused)
            {
                Time.timeScale = 0;
                showPaused();
                gamePaused = !gamePaused;
            }
            else if (gamePaused)
            {
                Time.timeScale = 1;
                hidePaused();
            }
        }
    }

    // This function mutes or unmuted the effects volume, according to the current value
    public void effectsIconClick()
    {
        if (AudioListener.volume == 0)
        {
            if (lastEVolume == 0)
                lastEVolume = 1;
            AudioListener.volume = lastEVolume;
            volumeSlider.value = lastEVolume;
            setEffectsIcon();
        }
        else
        {
            var aux = AudioListener.volume;
            AudioListener.volume = 0;
            volumeSlider.value = 0;
            setEffectsIcon();
            lastEVolume = aux;
        }
        PersistentValues.effectsVolume = volumeSlider.value;
        PersistentValues.effectsLastVolume = lastEVolume;
    }

    // This function is called when we change the effects slider value
    public void effectsChangeCheck()
    {
        // We set the AudioListener volume according to the effects slider
        AudioListener.volume = volumeSlider.normalizedValue;
        lastEVolume = AudioListener.volume;

        PersistentValues.effectsVolume = volumeSlider.value;
        PersistentValues.effectsLastVolume = lastEVolume;
        setEffectsIcon();
    }

    // This function sets the correct icon volume
    private void setEffectsIcon()
    {
        // Different icon is shown according to the volume
        if (volumeSlider.value == 0)
            effectsIcon.sprite = soundMuted;
        else if (volumeSlider.normalizedValue < 0.5f)
            effectsIcon.sprite = soundLow;
        else if (volumeSlider.normalizedValue < 1.0f)
            effectsIcon.sprite = soundMedium;
        else
            effectsIcon.sprite = soundFull;
    }

    // This function mutes or unmuted the music volume, according to the current value
    public void musicIconClick()
    {
        if (musicSlider.value == 0)
        {
            if (lastMVolume == 0)
                lastMVolume = 1;
            musicSlider.value = lastMVolume;
            setMusicIcon();
        }
        else
        {
            var aux = musicSlider.value;
            musicSlider.value = 0;
            setMusicIcon();
            lastMVolume = aux;
        }
        PersistentValues.musicVolume = musicSlider.value;
        PersistentValues.musicLastVolume = lastMVolume;
    }

    // This function is called everytime we change the music slider value
    public void musicChangeCheck()
    {
        lastMVolume = musicSlider.value;
        setMusicIcon();

        PersistentValues.musicVolume = musicSlider.value;
        PersistentValues.musicLastVolume = lastMVolume;
    }

    // This function sets the correct icon volume
    private void setMusicIcon()
    {
        // Different icon is shown according to the volume
        if (musicSlider.value == 0)
            musicIcon.sprite = soundMuted;
        else if (musicSlider.normalizedValue < 0.5f)
            musicIcon.sprite = soundLow;
        else if (musicSlider.normalizedValue < 1.0f)
            musicIcon.sprite = soundMedium;
        else
            musicIcon.sprite = soundFull;
    }

    //Reloads the Level
    public void Reload()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    //controls the pausing of the scene
    public void pauseControl()
    {
        Time.timeScale = 1;
        hidePaused();
    }

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        foreach (var g in pauseObjects)
            g.SetActive(true);
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (var g in pauseObjects)
            g.SetActive(false);
        Time.timeScale = 1;
        gamePaused = !gamePaused; // we need to change de boolean here
    }

    //loads inputted level
    public void LoadLevel(string level)
    {
        Application.LoadLevel(level);
    }
}