using UnityEngine;
using System.Collections;

public class timer : MonoBehaviour {

	public static timer instance;
	private float startTime;

	void Awake()
	{
		if (instance == null) {
			instance = this;
			return;
		}
	}

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
	}

	public string getTime(){
		float t = Time.time - startTime;
		string horas = (((int)t / 3600) % 24).ToString("00");
		string minutes = ((int)t / 60).ToString ("00");
		string seconds = (t % 60).ToString ("00");

		return horas  + ":" + minutes + ":" + seconds;
	}
}
