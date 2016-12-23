using UnityEngine;
using System.Collections;

public class SlideController : MonoBehaviour {
	public GameObject slideRegion;

	private int slide;
	// Use this for initialization
	void Start () {
		slide = 1;
		slideRegion.transform.FindChild ("Slide " + slide).gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void nextSlide(){
		slideRegion.transform.FindChild ("Slide " + slide).gameObject.SetActive (false);
		slide++;
		if (slideRegion.transform.FindChild ("Slide " + slide) == null) {
			slide = 1;
		}
		slideRegion.transform.FindChild ("Slide " + slide).gameObject.SetActive (true);
	}
}
