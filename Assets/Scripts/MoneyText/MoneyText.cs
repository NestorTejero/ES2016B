using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyText : MonoBehaviour {
	private float speed;
	private Vector3 direction;
	private float fadeTime;
	
	// Update is called once per frame
	void Update () {
		float translation = speed * Time.deltaTime;
		transform.Translate (direction * translation);
	}

	public void Initialize(float speed, Vector3 direction, float fadeTime){
		this.speed = speed;
		this.direction = direction;
		this.fadeTime = fadeTime;

		StartCoroutine (FadeOut());
	}

	private IEnumerator FadeOut(){
		float startAlpha = GetComponent<Text> ().color.a;

		float rate = 1.0f / fadeTime;
		float progress = 0.0f;

		while(progress < 1.0){
			Color tmpColor = GetComponent<Text> ().color;
			GetComponent<Text> ().color = new Color (tmpColor.r, tmpColor.g, tmpColor.b, Mathf.Lerp (startAlpha, 0.0f, progress));
			progress += rate*Time.deltaTime;
			yield return null;
		}

		Destroy (gameObject);
	}

}
