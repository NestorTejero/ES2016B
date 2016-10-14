using UnityEngine;
using System.Collections;

public class DayLightCycle : MonoBehaviour {

	public float dayLength;
	public float nightLength;
	public float time;
	public float hour;

	public Transform ubCenter;
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
	
	// Update is called once per frame
	void FixedUpdate () {
		sun.transform.RotateAround (ubCenter.transform.position,Vector3.right,Time.deltaTime/time);
		sun.transform.LookAt (ubCenter);

		moon.transform.RotateAround (ubCenter.transform.position,Vector3.right,Time.deltaTime/time);
		moon.transform.LookAt (ubCenter);

		stars.transform.Rotate (Vector3.right*(Time.deltaTime/4));

		hour += Time.deltaTime / time;

		if (hour >= 360) {
			hour = 0.0f;
		}

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
