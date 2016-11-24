using UnityEngine;
using UnityEngine.UI;

public class SoundOptions : MonoBehaviour
{
    public Image effectsIcon;
    private float lastEVolume; // Effects volume

    private float lastMVolume; // Music volume

    // Menu icons:
    public Image musicIcon;
    public Slider musicSlider;

    public Sprite soundFull;
    public Sprite soundLow;
    public Sprite soundMedium;
    public Sprite soundMuted;
    //public PersistentVars Vars;

    // Control of volume:
    public Slider volumeSlider;

    // Use this for initialization
    private void Start()
    {
        volumeSlider.onValueChanged.AddListener(delegate { effectsChangeCheck(); });
        musicSlider.onValueChanged.AddListener(delegate { musicChangeCheck(); });
        volumeSlider.value = PersistentValues.effectsVolume;
        musicSlider.value = PersistentValues.musicVolume;
        lastMVolume = PersistentValues.musicLastVolume;
        lastEVolume = PersistentValues.effectsLastVolume;
    }

    public void effectsChangeCheck()
    {
        // We set the AudioListener volume according to the effects slider
        //AudioListener.volume = volumeSlider.normalizedValue;
        lastEVolume = volumeSlider.normalizedValue;

        setEffectsIcon();

        PersistentValues.effectsVolume = volumeSlider.value;
        PersistentValues.effectsLastVolume = lastEVolume;
    }


    // Changes the effects sound
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

    public void musicChangeCheck()
    {
        AudioListener.volume = musicSlider.normalizedValue;
        lastMVolume = musicSlider.value;
        setMusicIcon();

        PersistentValues.musicVolume = musicSlider.value;
        PersistentValues.musicLastVolume = lastMVolume;
    }


    // Changes the music sound
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


    public void effectsIconClick()
    {
        if (volumeSlider.value == 0)
        {
            if (lastEVolume == 0)
                lastEVolume = 1;
            //AudioListener.volume = lastEVolume;
            volumeSlider.value = lastEVolume;
            setEffectsIcon();
        }
        else
        {
            var aux = volumeSlider.value;
            //AudioListener.volume = 0;
            volumeSlider.value = 0;
            setEffectsIcon();
            lastEVolume = aux;
        }

        PersistentValues.effectsVolume = volumeSlider.value;
        PersistentValues.effectsLastVolume = lastEVolume;
    }

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
}