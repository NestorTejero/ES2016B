using UnityEngine;
using System.Collections;

public class DayLightCycle : MonoBehaviour {

	//This variables set the length of the day and night
	public float dayLength;
	public float nightLength;
	public float time;	//This one will be equal to dayLength or NightLength depending on if it's night or day
	public float hour;	//This will give the hour assuming the clock has the 24h in the 360 degrees

	public Transform ubCenter; //This is the spot the light will be focusing while it's turning
	public Light sun;
	public Light moon;

	public ParticleSystem stars;
	private bool starsOn;

	// Use this for initialization
	void Start () {
		dayLength = 2.0f;
		nightLength = 1.0f;
		time = 2.0f;
		hour = 0.0f;
		starsOn = true;
	}
	
	// Using fixedUpdate to smooth camera issues
	void FixedUpdate () {

		//This calculates the rotation of the sun and moon.
		sun.transform.RotateAround (ubCenter.transform.position,Vector3.right,Time.deltaTime/time);
		sun.transform.LookAt (ubCenter);

		moon.transform.RotateAround (ubCenter.transform.position,Vector3.right,Time.deltaTime/time);
		moon.transform.LookAt (ubCenter);

		//the stars will rotate as well to make a nicer efect
		stars.transform.Rotate (Vector3.right*(Time.deltaTime/4));

		hour += Time.deltaTime / time;

		//Checking if it's a new day
		if (hour >= 360) {
			hour = 0.0f;
		}

		//Assuming the day it's splited in half with the night, we change lights intensities
		//during all the process and play/stop the stars particleSystem
		if (hour >= 180) {
			//Starting night time
			time = nightLength;
			sun.intensity -= Time.deltaTime;
			moon.intensity += Time.deltaTime;
			if (starsOn == false) {
				stars.Play ();
				starsOn = true;
			}

		} else {
			//Starting day time
			time = dayLength;
			sun.intensity += Time.deltaTime;
			moon.intensity -= Time.deltaTime;
			if (starsOn == true) {
				stars.Stop ();
				stars.Clear ();
				starsOn = false;
			}
		}

		//finally, including limits on the max and min intensity of the lights
		if (sun.intensity >= 0.9f) {
			sun.intensity = 0.9f;
		}
		if (sun.intensity <= 0.2f) {
			sun.intensity = 0.2f;
		}

		if (moon.intensity >= 0.5f) {
			moon.intensity = 0.5f;
		}
		if (moon.intensity <= 0.1f) {
			moon.intensity = 0.1f;
		}

	}
}
