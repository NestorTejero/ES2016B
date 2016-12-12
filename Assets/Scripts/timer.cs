using UnityEngine;

public class timer : MonoBehaviour
{
    public static timer instance;
    private float startTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    private void Start()
    {
        startTime = Time.time;
    }

    public string getTime()
    {
        var t = Time.time - startTime;
        var horas = ((int) t/3600%24).ToString("00");
        var minutes = ((int) t/60).ToString("00");
        var seconds = (t%60).ToString("00");

        return horas + ":" + minutes + ":" + seconds;
    }
}